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
    public class ConnectionHelper
    {
        protected string _serverURL = null;
        protected string _username = null; // username 
        protected string _password = null;
        protected bool _anonymous = false;
        protected string _sessionKey = null; //current session key
        private Error _lastError = null;
        private string _userAgent = null;

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
        #endregion

        #region ConnectionHelper Factory
        private static Dictionary<string, ConnectionHelper> _connectionHelpers = new Dictionary<string, ConnectionHelper>();
        public static ConnectionHelper GetConnectionHelper(string serverURL, string username = null, string password = null)
        {
            ConnectionHelper connectionHelper = null;
            if (_connectionHelpers.ContainsKey(serverURL))
            {
                connectionHelper = _connectionHelpers[serverURL];
            }
            else
            {
                if (username == null && password == null)
                {
                    connectionHelper = new ConnectionHelper(serverURL);
                }
                else
                {
                    connectionHelper = new ConnectionHelper(serverURL, username, password);
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
        protected ConnectionHelper(string serverURL)
        {
            _userAgent = "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            _username = Constant.REST_ANONYMOUS_USERNAME;
            isAnonymous();
            _serverURL = serverURL;
            LastResponseHeaders = new ResponseHeaders();
        }

        protected ConnectionHelper(string serverURL, string username, string password)
            : this(serverURL)
        {
            _username = username;
            _password = password;
            isAnonymous();
        }
        #endregion

        #region Authorization
        private string authHeaderString(string username, string password)
        {
            return "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
        }

        private CredentialCache standardCredentials(string url)
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

        public void LogoutCurrentSession(int retryIndex = 0)
        {
            _username = Constant.REST_ANONYMOUS_USERNAME;
            isAnonymous();
            if (String.IsNullOrEmpty(_sessionKey))
                return;
            string validationUrl = _serverURL;
            validationUrl += Constant.REST_BASE_PATH + Constant.REST_AUTHENTICATE_URL_EXTENSION[retryIndex] + Constant.REST_LOGOUT_EXTENSION;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(validationUrl);
            request.Headers.Add(Constant.HEADER_SESSIONKEY, _sessionKey);
            request.Timeout = Constant.REST_AUTHENTICATE_TIMEOUT;
            request.UserAgent = _userAgent;//Constant.REST_USER_AGENT;
            request.Method = "HEAD";
            try
            {
                response = getResponse(request);
            }
            catch (WebException e)
            {
                // Doesn't matter if this fails for now
                if (httpRetryValid(request, e) && retryIndex < Constant.REST_AUTHENTICATE_URL_EXTENSION.Length)
                {
                    LogoutCurrentSession(++retryIndex);
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        public bool NewSession(string username, string password)
        {
            _password = password;
            _username = username;
            isAnonymous();
            return ValidateCredentials();
        }

        public bool IsLoggedIn()
        {
            bool result = false;
            try
            {
                result = ValidateCredentials();
            }
            catch (RESTAPIException e)
            {
                result = false;
            }
            return !isAnonymous() && result;
        }

        private bool isAnonymous()
        {
            _anonymous = Constant.REST_ANONYMOUS_USERNAME.Equals(_username) ? true : false;
            return _anonymous;
        }

        public bool ValidateCredentials(int retryIndex = 0)
        {
            string username = _username;
            string password = _password;
            string serverAddress = _serverURL;
            string sessionKey = _sessionKey;

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
            request.Timeout = Constant.REST_AUTHENTICATE_TIMEOUT;
            request.UserAgent = _userAgent;//Constant.REST_USER_AGENT;
            request.Method = "HEAD";

            try
            {
                response = getResponse(request);
                //string validUser = response.Headers[Constant.HEADER_USERNAME];
                string validUser = LastResponseHeaders.Username;
                string lastSessionKey = LastResponseHeaders.SessionKey;

                //if (options != null)
                //options.OA_Version = response.Headers[Constant.HEADER_OPENASSET_VERSION];

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
                    //CurrentUsername = validUser;
                    //LastSuccessfulValidation = DateTime.Now;
                    return true;
                }

                if (validUser != null && validUser.Equals(username))
                {
                    // if it is a valid user keep the session
                    if (!String.IsNullOrEmpty(lastSessionKey))
                        _sessionKey = lastSessionKey;
                    //if (!String.IsNullOrEmpty(response.Headers[Constant.HEADER_SESSIONKEY]))
                    //_sessionKey = response.Headers[Constant.HEADER_SESSIONKEY];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebException e)
            {
                if (httpRetryValid(request, e) && retryIndex < Constant.REST_AUTHENTICATE_URL_EXTENSION.Length)
                {
                    return ValidateCredentials(++retryIndex);
                }
                /*if (options != null && e.Response != null)
                {
                    options.OA_Version = e.Response.Headers[Constant.HEADER_OPENASSET_VERSION];
                }*/
                MarshallError(validationUrl, e);
            }
            catch (Exception e)
            {
                MarshallError(validationUrl, e);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

            isAnonymous();
            return false;
        }
        #endregion

        #region Error handling
        private bool httpRetryValid(HttpWebRequest request, WebException we)
        {
            HttpWebResponse errorResponse = we.Response as HttpWebResponse;
            if (errorResponse == null)
                return false;
            bool anonLoginEnabled = Convert.ToBoolean(isAnonymous());
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

        protected void MarshallError(string openAssetUrl, Exception e)
        {
            if (e is WebException && (e as WebException).Status == WebExceptionStatus.ProtocolError)
            {
                HttpWebResponse errorResponse = (HttpWebResponse)(e as WebException).Response;
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
                    _lastError.HttpStatusCode = (int)((e as WebException).Response as HttpWebResponse).StatusCode;
                    _lastError.ErrorMessage = responseText;
                }
            }
            else if (e is WebException)
            {
                _lastError = new Error();
                _lastError.HttpStatusCode = (int)(e as WebException).Status;
                _lastError.ErrorMessage = e.Message;
            }
            else
            {
                _lastError = new Error();
                _lastError.HttpStatusCode = -1;
                _lastError.ErrorMessage = e.Message;
            }

            throw new RESTAPIException(openAssetUrl, _lastError, e);
        }
        #endregion

        #region Response
        private HttpWebResponse getResponse(HttpWebRequest request, bool ignoreUsername = false)
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
                        new Exception(message));
                }
            }
            setLastResponseHeaders(responseHeader);
            return response;
        }

        private void setLastResponseHeaders(WebHeaderCollection headerCollection)
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

        private HttpWebResponse getRESTResponse(string url, string method, byte[] output = null, bool retry = false, string contentType = "application/json")
        {
            HttpWebResponse response = null;

            // HTTP REQUEST
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.UserAgent = _userAgent;//Constant.REST_USER_AGENT;
            request.Timeout = Constant.REST_REQUEST_TIMEOUT;
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
                //CurrentUsername = response.Headers[Constant.HEADER_USERNAME];
            }
            catch (WebException e)
            {
                if (httpRetryValid(request, e))
                {
                    return getRESTResponse(url, method, output, true, contentType);
                }
                MarshallError(url, e);
                throw;
            }
            catch (Exception e)
            {
                MarshallError(url, e);
                throw;
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

        //private static readonly Encoding encoding = Encoding.UTF8;
        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
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

        public bool MeetsRESTRequirement(string oaVersion = null)
        {
            if (String.IsNullOrEmpty(oaVersion))
            {
                oaVersion = LastResponseHeaders.OpenAssetVersion;
                if (String.IsNullOrEmpty(oaVersion))
                {
                    try
                    {
                        ValidateCredentials();
                        oaVersion = LastResponseHeaders.OpenAssetVersion;
                    } catch (Exception e) 
                    {
                        oaVersion = null;
                    }
                }
            }
            if (!String.IsNullOrEmpty(oaVersion))
            {
                oaVersion = oaVersion.Replace("h", "");
                string[] curVersion = oaVersion.Split('.');
                string[] minVersion = Constant.REST_MIN_VERSION.Split('.');
                if (minVersion.Length != curVersion.Length)
                    return false;
                for (int i = 0; i < minVersion.Length && i < curVersion.Length; i++)
                {
                    int min = Convert.ToInt32(minVersion[i]);
                    int cur = Convert.ToInt32(curVersion[i]);
                    if (min > cur)
                        return false;
                    if (cur > min)
                        return true;
                }
                return true;
            }

            return false;
        }
        #endregion

        #region Get/Send objects
        #region GET Objects
        public T GetObject<T>(int id, RESTOptions<T> options) where T : Noun.Base.BaseNoun, new()
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

        public List<T> GetObjects<T>(RESTOptions<T> options) where T : Noun.Base.BaseNoun, new()
        {
            return GetObjects<T>(0, null, options);
        }

        public List<T> GetObjects<T>(int id, string parentNoun, RESTOptions<T> options) where T : Noun.Base.BaseNoun, new()
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

                //options.DisplayedResults = Convert.ToInt32(response.Headers[Constant.HEADER_DISPLAY_RESULTS_COUNT]);
                //options.TotalResults = Convert.ToInt32(response.Headers[Constant.HEADER_FULL_RESULTS_COUNT]);

                TextReader tr = new StreamReader(response.GetResponseStream());
                string responseText = tr.ReadToEnd();
                tr.Close();
                tr.Dispose();
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
        private string sendObjectStringResponse(byte[] output, bool createNew, string urlNoun, string contentType)
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

        private T deserealizeResponse<T>(string response, bool createNew) where T : Noun.Base.BaseNoun, new()
        {
            T value = null;
            if (createNew)
            {
                value = JsonConvert.DeserializeObject<T>(response);
            }
            else
            {
                value = JsonConvert.DeserializeObject<T>(response);
            }
            return value;
        }

        public T SendObject<T>(T sendingObject, bool createNew = false) where T : Noun.Base.BaseNoun, new()
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
            T value = deserealizeResponse<T>(responseText, createNew);
            return value;
        }


        // any base noun can be used but only the FileNoun accepts this type of POST
        public T SendObject<T>(T sendingObject, string filepath) where T : Noun.Base.BaseNoun, new()
        {
            // read file
            string filename = Path.GetFileName(filepath);
            string fileExtension = Path.GetExtension(filename).Remove(0, 1);
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();
            // serialize sending object
            string jsonOut = JsonConvert.SerializeObject(sendingObject);//
            // generate post object
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("file", new FileParameter(data, filename, "image/" + fileExtension));
            postParameters.Add("_jsonBody", jsonOut);

            // form data
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;
            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            // send post/put request
            string urlNoun = "/" + Noun.Base.BaseNoun.GetNoun(typeof(T));
            string responseText = sendObjectStringResponse(formData, true, urlNoun, contentType);
            // deserealize object
            T value = deserealizeResponse<T>(responseText, true);
            return value;
        }

        public List<T> SendObjects<T>(List<T> sendingObject, bool createNew = false) where T : Noun.Base.BaseNoun, new()
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
    }
}
