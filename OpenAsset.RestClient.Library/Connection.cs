using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace OpenAsset.RestClient.Library
{
    public class Connection
    {
        protected string _serverURL = null;
        protected string _username = null; // username 
        protected string _password = null;
        protected bool _anonymous = false;
        protected string _sessionKey = null; //current session key
        protected Error _lastError = null;
        protected string _userAgent = null;
        protected int _authenticationTimeout = Constant.DEFAULT_REST_AUTHENTICATE_TIMEOUT;
        protected int _requestTimeout = Constant.DEFAULT_REST_REQUEST_TIMEOUT;
        protected int _lastValidationEndpoint = 0;
        protected bool _forceProxyBypass = false;

        //values from the last request made
        // if the last request didn't had the value it is empty
        public struct ResponseHeaders
        {
            public int? DisplayResultsCount;
            public int? FullResultsCount;
            public string OpenAssetVersion;
            public int? Offset;
            public string SessionKey; // last response session key (shouldn't be different from the current)
            //public int Timing; // only in development
            public int? UserId;
            public string Username;
        }
        public ResponseHeaders LastResponseHeaders;

        #region Accessors
        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string SessionKey
        {
            get { return _sessionKey; }
        }

        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        public int AuthenticationTimeout
        {
            get { return _authenticationTimeout; }
            set
            {
                if (value < 0)
                    _authenticationTimeout = Constant.DEFAULT_REST_AUTHENTICATE_TIMEOUT;
                _authenticationTimeout = value;
            }
        }

        public int RequestTimeout
        {
            get { return _requestTimeout; }
            set
            {
                if (value < 0)
                    _requestTimeout = Constant.DEFAULT_REST_REQUEST_TIMEOUT;
                _requestTimeout = value;
            }
        }

        public bool ForceProxyBypass
        {
            get { return _forceProxyBypass; }
            set { _forceProxyBypass = value; }
        }
        #endregion

        #region Connection Factory
        protected static Dictionary<string, Connection> _connectionHelpers = new Dictionary<string, Connection>();
        public static Connection GetConnection(string serverURL, string username = null, string password = null)
        {
            Connection connectionHelper = null;
            if (_connectionHelpers.ContainsKey(serverURL))
            {
                connectionHelper = _connectionHelpers[serverURL];
            }
            else
            {
                if (username == null && password == null)
                {
                    connectionHelper = new Connection(serverURL);
                }
                else
                {
                    connectionHelper = new Connection(serverURL, username, password);
                }
                _connectionHelpers.Add(serverURL, connectionHelper);
            }
            //if URL exists but username and password different start a new session
            if (!connectionHelper._password.Equals(password) ||
                !connectionHelper._username.Equals(username) ||
                !connectionHelper._serverURL.Equals(serverURL))
            {
                connectionHelper.LogoutCurrentSession();
                connectionHelper.NewSession(username, password);
            }
            return connectionHelper;
        }
        #endregion

        #region Constructors
        protected Connection(string serverURL)
        {
            _userAgent = "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            _username = Constant.REST_ANONYMOUS_USERNAME;
            _serverURL = serverURL;
            LastResponseHeaders = new ResponseHeaders();
        }

        protected Connection(string serverURL, string username, string password)
            : this(serverURL)
        {
            _username = username;
            _password = password;
        }
        #endregion

        #region Authorization
        protected string authHeaderString(string username, string password)
        {
            return "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
        }

        protected virtual CredentialCache standardCredentials(string url)
        {
            CredentialCache cc = new CredentialCache();
            if (isAnonymous())
                return cc;
            cc.Add(new Uri(url), "NTLM", CredentialCache.DefaultNetworkCredentials);
            if (!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_password))
            {
                cc.Add(new Uri(url), "Basic", new NetworkCredential(_username, _password));
            }
            return cc;
        }

        public virtual void LogoutCurrentSession()
        {
            _username = Constant.REST_ANONYMOUS_USERNAME;
            if (String.IsNullOrEmpty(_sessionKey))
                return;
            string validationUrl = _serverURL;
            validationUrl += Constant.REST_BASE_PATH + Constant.REST_AUTHENTICATE_URL_EXTENSION[_lastValidationEndpoint] + Constant.REST_LOGOUT_EXTENSION;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(validationUrl);
            request.Headers.Add(Constant.HEADER_SESSIONKEY, _sessionKey);
            request.Timeout = AuthenticationTimeout;
            request.UserAgent = _userAgent;
            if (_forceProxyBypass)
                request.Proxy = new WebProxy();
            request.Method = "HEAD";
            try
            {
                response = getResponse(request, true);
            }
            catch (WebException)
            {
                // Doesn't matter if this fails as logout is always accepted
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        public virtual bool NewSession(string username, string password, bool forceValidate = true)
        {
            _password = password;
            _username = username;
            if (forceValidate)
                return ValidateCredentials();
            return false;
        }

        public virtual bool IsLoggedIn()
        {
            bool result = false;
            try
            {
                result = ValidateCredentials();
            }
            catch (RESTAPIException)
            {
                result = false;
            }
            return !isAnonymous() && result;
        }

        protected virtual bool isAnonymous()
        {
            _anonymous = Constant.REST_ANONYMOUS_USERNAME.Equals(_username);
            return _anonymous;
        }

        public virtual bool ValidateCredentials(int retryIndex = 0)
        {
            string username = _username;
            string password = _password;
            string serverAddress = _serverURL;
            string sessionKey = _sessionKey;

            if (retryIndex < _lastValidationEndpoint)
                retryIndex = _lastValidationEndpoint;

            string validationUrl = serverAddress + Constant.REST_BASE_PATH + Constant.REST_AUTHENTICATE_URL_EXTENSION[retryIndex];
            HttpWebResponse response = null;
            HttpWebRequest request = null;

            request = (HttpWebRequest)WebRequest.Create(validationUrl);
            if (username == null || password == null)
            {
                request.Credentials = standardCredentials(validationUrl);
            }
            else if (!Constant.REST_ANONYMOUS_USERNAME.Equals(username))
            {
                request.Headers.Add("Authorization", authHeaderString(username, password));
            }
            if (!String.IsNullOrEmpty(sessionKey))
            {
                request.Headers.Add(Constant.HEADER_SESSIONKEY, sessionKey);
            }
            request.Timeout = AuthenticationTimeout;
            request.UserAgent = _userAgent;
            if (_forceProxyBypass)
                request.Proxy = new WebProxy();
            request.Method = "HEAD";

            try
            {
                response = getResponse(request);
                string validUser = LastResponseHeaders.Username;
                string lastSessionKey = LastResponseHeaders.SessionKey;

                if (username == null || password == null)
                {
                    if (validUser == null || (!validUser.Equals(username) && !validUser.Equals(CredentialCache.DefaultNetworkCredentials.UserName)))
                    {
                        if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
                        {
                            return ValidateCredentials();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }

                if (validUser != null && validUser.Equals(username))
                {
                    // if it is a valid user keep the session
                    if (!String.IsNullOrEmpty(lastSessionKey))
                        _sessionKey = lastSessionKey;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebException e)
            {
                if ((retryIndex + 1) < Constant.REST_AUTHENTICATE_URL_EXTENSION.Length && endpointNotFound(e))
                {
                    retryIndex++;
                    _lastValidationEndpoint = retryIndex;
                    return ValidateCredentials(retryIndex);
                }
                if (httpRetryValid(request, e))
                {
                    return ValidateCredentials(retryIndex);
                }
                try
                {
                    MarshallError(validationUrl, e);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                try
                {
                    MarshallError(validationUrl, e);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

            return false;
        }
        #endregion

        #region Error handling
        protected bool httpRetryValid(HttpWebRequest request, WebException we)
        {
            HttpWebResponse errorResponse = we.Response as HttpWebResponse;
            if (errorResponse == null)
                return false;
            bool anonLoginEnabled = isAnonymous();
            string username = null, password = null;
            string authorization = request.Headers["Authorization"];
            if (authorization != null && authorization.StartsWith("Basic "))
            {
                authorization = authorization.Substring(6);
                string[] credentials = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(authorization)).Split(new Char[] { ':' }, 2);
                if (credentials.Length == 2)
                {
                    username = credentials[0];
                    password = credentials[1];
                }
                else
                {
                    return true;
                }
            }
            if (authorization != null && authorization.StartsWith("NTLM "))
            {
                // Failed NTLM attempt, allow basic auth
                return true;
            }
            if (errorResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                if (!anonLoginEnabled && !String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_password))
                {
                    if (!_username.Equals(errorResponse.Headers[Constant.HEADER_USERNAME]) && !_username.Equals(username) && !_password.Equals(password))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected bool endpointNotFound(WebException we)
        {
            HttpWebResponse errorResponse = we.Response as HttpWebResponse;
            if (errorResponse == null)
                return false;
            if (errorResponse.StatusCode == HttpStatusCode.NotFound || errorResponse.StatusCode == HttpStatusCode.InternalServerError)
                return true;
            return false;
        }

        protected void MarshallError(string openAssetUrl, Exception e, WebRequest request = null)
        {
            WebException we = e as WebException;
            if (we != null && we.Status == WebExceptionStatus.ProtocolError)
            {
                HttpWebResponse errorResponse = we.Response as HttpWebResponse;
                setLastResponseHeaders(errorResponse.Headers);
                TextReader tr = new StreamReader(errorResponse.GetResponseStream());
                string responseText = tr.ReadToEnd();
                tr.Close();
                tr.Dispose();

                try
                {
                    _lastError = JsonConvert.DeserializeObject<Error>(responseText);
                }
                catch (JsonException)
                {
                    _lastError = new Error();
                    _lastError.HttpStatusCode = (int)(we.Response as HttpWebResponse).StatusCode;
                    _lastError.ErrorMessage = responseText;
                }
            }
            else if (we != null)
            {
                _lastError = new Error();
                _lastError.HttpStatusCode = (int)we.Status;
                _lastError.ErrorMessage = e.Message;
            }
            else
            {
                _lastError = new Error();
                _lastError.HttpStatusCode = -1;
                _lastError.ErrorMessage = e.Message;
            }

            throw new RESTAPIException(openAssetUrl, _lastError, e, request);
        }
        #endregion

        #region Response
        protected virtual HttpWebResponse getResponse(HttpWebRequest request, bool ignoreUsername = false)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            WebHeaderCollection responseHeader = response.Headers;
            string validUser = responseHeader[Constant.HEADER_USERNAME];
            if (!ignoreUsername)
            {
                if (!validUser.Equals(_username))
                {
                    string message = "Username of response differs from username in request";
                    throw new NotValidUserException(
                        response.ResponseUri.ToString(),
                        new Exception(message),
                        request);
                }
            }
            setLastResponseHeaders(responseHeader);
            return response;
        }

        protected virtual void setLastResponseHeaders(WebHeaderCollection headerCollection)
        {
            LastResponseHeaders.OpenAssetVersion = headerCollection[Constant.HEADER_OPENASSET_VERSION];
            LastResponseHeaders.Username = headerCollection[Constant.HEADER_USERNAME];
            LastResponseHeaders.SessionKey = headerCollection[Constant.HEADER_SESSIONKEY];
            //LastRequestHeaders.Timing = Convert.ToInt32(headerCollection[Constant.HEADER_TIMING]);//development
            if (String.IsNullOrEmpty(headerCollection[Constant.HEADER_DISPLAY_RESULTS_COUNT]))
            {
                LastResponseHeaders.DisplayResultsCount = null;
            }
            else
            {
                LastResponseHeaders.DisplayResultsCount = Convert.ToInt32(headerCollection[Constant.HEADER_DISPLAY_RESULTS_COUNT]);
            }
            if (String.IsNullOrEmpty(headerCollection[Constant.HEADER_FULL_RESULTS_COUNT]))
            {
                LastResponseHeaders.FullResultsCount = null;
            }
            else
            {
                LastResponseHeaders.FullResultsCount = Convert.ToInt32(headerCollection[Constant.HEADER_FULL_RESULTS_COUNT]);
            }
            if (String.IsNullOrEmpty(headerCollection[Constant.HEADER_OFFSET]))
            {
                LastResponseHeaders.Offset = null;
            }
            else
            {
                LastResponseHeaders.Offset = Convert.ToInt32(headerCollection[Constant.HEADER_OFFSET]);
            }
            if (String.IsNullOrEmpty(headerCollection[Constant.HEADER_USER_ID]))
            {
                LastResponseHeaders.UserId = null;
            }
            else
            {
                LastResponseHeaders.UserId = Convert.ToInt32(headerCollection[Constant.HEADER_USER_ID]);
            }
        }

        protected virtual HttpWebResponse getRESTResponse(string url, string method, byte[] output = null, bool retry = false, string contentType = "application/json")
        {
            HttpWebResponse response = null;

            // HTTP REQUEST
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;
            request.Method = method;
            request.UserAgent = _userAgent;
            if (_forceProxyBypass)
                request.Proxy = new WebProxy();
            request.Timeout = RequestTimeout;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentType = contentType;

            if (!String.IsNullOrEmpty(_sessionKey))
            {
                request.Headers.Add(Constant.HEADER_SESSIONKEY, _sessionKey);
            }
            if (!isAnonymous())
            {
                if (retry)
                {
                    request.Headers.Add("Authorization", authHeaderString(_username, _password));
                }
                else
                {
                    request.Credentials = standardCredentials(url);
                }
            }
            try
            {
                if (output != null && output.Length > 0)
                {
                    request.ContentLength = output.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(output, 0, output.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
                response = getResponse(request, retry);
                if (!String.IsNullOrEmpty(LastResponseHeaders.SessionKey))
                {
                    _sessionKey = LastResponseHeaders.SessionKey;
                }
            }
            catch (WebException e)
            {
                if (!retry && httpRetryValid(request, e))
                {
                    return getRESTResponse(url, method, output, true, contentType);
                }
                MarshallError(url, e, request);
            }
            catch (Exception e)
            {
                MarshallError(url, e, request);
            }


            return response;
        }
        #endregion

        #region Multipart Form methods
        // Copied from: http://www.briangrinstead.com/blog/multipart-form-post-in-c
        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }

        protected static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
        #endregion

        #region Helpers
        public Error RetrieveLastError()
        {
            return _lastError;
        }

        public void ClearLastError()
        {
            _lastError = null;
        }

        public virtual bool MeetsRESTRequirement(string oaVersion = null, string versionToCheck = Constant.REST_MIN_VERSION)
        {
            // Check that versionToCheck is of the format #.#.#
            if (String.IsNullOrEmpty(versionToCheck))
                return false;

            if (String.IsNullOrEmpty(oaVersion))
            {
                oaVersion = LastResponseHeaders.OpenAssetVersion;
                if (String.IsNullOrEmpty(oaVersion))
                {
                    try
                    {
                        ValidateCredentials();
                        oaVersion = LastResponseHeaders.OpenAssetVersion;
                    }
                    catch (Exception)
                    {
                        oaVersion = null;
                    }
                }
            }
            if (!String.IsNullOrEmpty(oaVersion))
            {
                oaVersion = oaVersion.Replace("h", "");
                try
                {
                    Version minVersion = new Version(versionToCheck);
                    Version curVersion = new Version(oaVersion);
                    return curVersion >= minVersion;
                }
                catch (Exception)
                {
                    // One of the versions did not parse correctly
                    return false;
                }
            }

            return false;
        }

        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
        #endregion

        #region Get/Send objects
        #region GET Objects
        public virtual T GetObject<T>(int id, RESTOptions<T> options) where T : Noun.Base.BaseNoun, new()
        {
            HttpWebResponse response = null;
            try
            {
                string restUrl = _serverURL + Constant.REST_BASE_PATH + "/" + Noun.Base.BaseNoun.GetNoun(typeof(T)) + "/" + id + "?" + options.GetUrlParameters();
                response = getRESTResponse(restUrl, "GET");
                TextReader tr = new StreamReader(response.GetResponseStream());
                string responseText = tr.ReadToEnd();
                tr.Close();
                tr.Dispose();

                T objT = JsonConvert.DeserializeObject<T>(responseText);
                return objT;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        public virtual List<T> GetObjects<T>(RESTOptions<T> options) where T : Noun.Base.BaseNoun, new()
        {
            return GetObjects<T>(0, null, options);
        }

        public virtual List<T> GetObjects<T>(int id, string parentNoun, RESTOptions<T> options) where T : Noun.Base.BaseNoun, new()
        {
            HttpWebResponse response = null;
            try
            {
                string restUrl = _serverURL + Constant.REST_BASE_PATH;
                if (!String.IsNullOrEmpty(parentNoun))
                    restUrl += "/" + parentNoun;
                else
                    restUrl += "/" + Noun.Base.BaseNoun.GetNoun(typeof(T));
                if (id > 0)
                    restUrl += "/" + id;
                if (!String.IsNullOrEmpty(parentNoun))
                    restUrl += "/" + Noun.Base.BaseNoun.GetNoun(typeof(T));
                restUrl += "?" + options.GetUrlParameters();
                response = getRESTResponse(restUrl, "GET");

                TextReader tr = new StreamReader(response.GetResponseStream());
                string responseText = tr.ReadToEnd();
                tr.Close();
                tr.Dispose();
                //System.Console.WriteLine(responseText);
                return JsonConvert.DeserializeObject<List<T>>(responseText);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        #endregion

        #region SEND Objects
        protected virtual string sendObjectStringResponse(byte[] output, bool createNew, string urlNoun, string contentType)
        {
            string responseText;
            HttpWebResponse response = null;
            try
            {
                string restUrl = _serverURL + Constant.REST_BASE_PATH + urlNoun;
                string method = createNew ? "POST" : "PUT";

                response = getRESTResponse(restUrl, method, output, true, contentType);
                // get response data
                TextReader tr = new StreamReader(response.GetResponseStream());
                responseText = tr.ReadToEnd();
                tr.Close();
                tr.Dispose();
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return responseText;
        }

        protected virtual T deserealizeResponse<T>(string response) where T : Noun.Base.BaseNoun, new()
        {
            T value = JsonConvert.DeserializeObject<T>(response);
            return value;
        }

        public virtual T SendObject<T>(T sendingObject, bool createNew = false) where T : Noun.Base.BaseNoun, new()
        {
            // serialize sending object
            string jsonOut = JsonConvert.SerializeObject(sendingObject);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] output = encoding.GetBytes(jsonOut);
            // send post/put request
            string urlNoun = "/" + Noun.Base.BaseNoun.GetNoun(typeof(T));
            urlNoun += createNew ? "" : "/" + sendingObject.Id;
            string contentType = "application/json";
            string responseText = sendObjectStringResponse(output, createNew, urlNoun, contentType);
            // deserealize object
            T value = deserealizeResponse<T>(responseText);
            return value;
        }

        // any base noun can be used but only the FileNoun accepts this type of POST
        public virtual T SendObject<T>(T sendingObject, string filepath, bool createNew = false) where T : Noun.Base.BaseNoun, new()
        {
            // read file
            string filename = Path.GetFileName(filepath);
            string mimeType = GetMimeType(filename);
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();
            return SendObject<T>(sendingObject, data, filename, mimeType, createNew);
        }

        // any base noun can be used but only the FileNoun accepts this type of POST
        public virtual T SendObject<T>(T sendingObject, byte[] data, string filename, string mimeType = null, bool createNew = false) where T : Noun.Base.BaseNoun, new()
        {
            if (mimeType == null)
                mimeType = "application/unknown";
            // serialize sending object
            string jsonOut = JsonConvert.SerializeObject(sendingObject);
            // generate post object
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("file", new FileParameter(data, filename, mimeType));
            postParameters.Add("_jsonBody", jsonOut);

            // form data
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;
            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            // send post/put request
            string urlNoun = "/" + Noun.Base.BaseNoun.GetNoun(typeof(T));
            urlNoun += createNew ? "" : "/" + sendingObject.Id;
            string responseText = sendObjectStringResponse(formData, createNew, urlNoun, contentType);
            // deserealize object
            T value = deserealizeResponse<T>(responseText);
            return value;
        }

        public virtual List<T> SendObjects<T>(List<T> sendingObject, bool createNew = false) where T : Noun.Base.BaseNoun, new()
        {
            // serialize sending object
            string jsonOut = JsonConvert.SerializeObject(sendingObject);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] output = encoding.GetBytes(jsonOut);
            // send post/put request
            string urlNoun = "/" + Noun.Base.BaseNoun.GetNoun(typeof(T));
            string contentType = "application/json";
            string responseText = sendObjectStringResponse(output, createNew, urlNoun, contentType);

            // fill values list
            List<T> values = JsonConvert.DeserializeObject<List<T>>(responseText);
            return values;
        }
        #endregion
        #endregion

        #region DELETE Objects
        // Empty response on success, throws error on failure
        public virtual void DeleteObject<T>(int id) where T : Noun.Base.BaseNoun
        {
            HttpWebResponse response = null;
            try
            {
                string restUrl = _serverURL + Constant.REST_BASE_PATH + "/" + Noun.Base.BaseNoun.GetNoun(typeof(T)) + "/" + id;
                response = getRESTResponse(restUrl, "DELETE");
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        #endregion

        #region OPTIONS Calls
        public virtual Information.Options GetOptions(Type type, int id = 0)
        {
            HttpWebResponse response = null;
            try
            {
                string restUrl = _serverURL + Constant.REST_BASE_PATH + "/" + Noun.Base.BaseNoun.GetNoun(type);
                if (id > 0)
                    restUrl += "/" + id;
                response = getRESTResponse(restUrl, "OPTIONS");
                TextReader tr = new StreamReader(response.GetResponseStream());
                string responseText = tr.ReadToEnd();
                tr.Close();
                tr.Dispose();

                Information.Options objT = JsonConvert.DeserializeObject<Information.Options>(responseText);
                return objT;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        public virtual Information.Options GetOptions(Noun.Base.BaseNoun noun)
        {
            return GetOptions(noun.GetType(), noun.Id);
        }
        #endregion
    }
}
