using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
// could have used the JavaScriptSerializer but the Newtonsoft seems to be faster
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    abstract public class RequestMethods
    {
        protected string _password;
        protected string _username;

        protected Stream httpRequestStream(HttpMethod method, string sURL, string sData, out int responseCode, string filePath)
        {
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            wrGETURL.Method = method.ToString();
            if (_username != null && _password != null)
            {
                // set the authorization header
                string credentials = String.Format("{0}:{1}", _username, _password);
                byte[] bytes = Encoding.ASCII.GetBytes(credentials);
                string base64 = Convert.ToBase64String(bytes);
                string authorization = String.Concat("Basic ", base64);
                wrGETURL.Headers.Add("Authorization", authorization);
            }

            if (filePath != null)
            {
                //inspired by http://www.techcoil.com/blog/uploading-large-http-multipart-request-with-system-net-httpwebrequest-in-c/
                string jsonBodyFormParameterName = "_jsonBody";
                string fileFormParameterName = "file";
                // set the file to upload (if it has a file, then it is an Image Post)
                string boundary = "------------------------" + DateTime.Now.Ticks;
                // Turn off the buffering of data to be written, to prevent
                // OutOfMemoryException when sending data
                ((HttpWebRequest)wrGETURL).AllowWriteStreamBuffering = false;
                // Specify that the content type is a multipart request
                wrGETURL.ContentType = "multipart/form-data; boundary=" + boundary;
                ((HttpWebRequest)wrGETURL).KeepAlive = false;

                //size of content to upload
                ASCIIEncoding ascii = new ASCIIEncoding();
                string boundaryStringLine = "\r\n--" + boundary + "\r\n";
                byte[] boundaryStringLineBytes = ascii.GetBytes(boundaryStringLine);
                string lastBoundaryStringLine = "\r\n--" + boundary + "--\r\n";
                byte[] lastBoundaryStringLineBytes = ascii.GetBytes(lastBoundaryStringLine);
                // Get the byte array of the jsonBody content disposition
                string jsonBodyContentDisposition = String.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", jsonBodyFormParameterName, sData);
                byte[] jsonBodyContentDispositionBytes = ascii.GetBytes(jsonBodyContentDisposition);
                // Get the byte array of the file content disposition
                string fileContentDisposition = String.Format(
                    "Content-Disposition: form-data;name=\"{0}\"; " + "filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n",
                    fileFormParameterName,
                    Path.GetFileName(filePath),
                    Path.GetExtension(filePath));
                byte[] fileContentDispositionBytes = ascii.GetBytes(fileContentDisposition);
                FileInfo fileInfo = new FileInfo(filePath);
                // Calculate the total size of the HTTP request
                long totalRequestBodySize = boundaryStringLineBytes.Length * 2
                    + lastBoundaryStringLineBytes.Length
                    + jsonBodyContentDispositionBytes.Length
                    + fileContentDispositionBytes.Length
                    + fileInfo.Length;
                // And indicate the value as the HTTP request content length
                wrGETURL.ContentLength = totalRequestBodySize;
                // Write the http request body directly to the server
                using (Stream s = wrGETURL.GetRequestStream())
                {
                    // Send the jsonBody content disposition over to the server
                    s.Write(boundaryStringLineBytes, 0, boundaryStringLineBytes.Length);
                    s.Write(jsonBodyContentDispositionBytes, 0,
                        jsonBodyContentDispositionBytes.Length);

                    // Send the file content disposition over to the server
                    s.Write(boundaryStringLineBytes, 0, boundaryStringLineBytes.Length);
                    s.Write(fileContentDispositionBytes, 0,
                        fileContentDispositionBytes.Length);

                    // Send the file binaries over to the server, in 1024 bytes chunk
                    FileStream fileStream = new FileStream(filePath, FileMode.Open,
                        FileAccess.Read);
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        s.Write(buffer, 0, bytesRead);
                    } // end while
                    fileStream.Close();

                    // Send the last part of the HTTP request body
                    s.Write(lastBoundaryStringLineBytes, 0, lastBoundaryStringLineBytes.Length);
                } // end using
            }
            else if (sData != null)
            {
                // set the raw data (in case there's only json)
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] arr = encoding.GetBytes(sData);
                wrGETURL.ContentType = "application/json";
                wrGETURL.ContentLength = arr.Length;
                Stream dataStream = wrGETURL.GetRequestStream();
                dataStream.Write(arr, 0, arr.Length);
                dataStream.Close();
            }
            
            // retrieve the response
            Stream objStream = null;
            responseCode = -1;
            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)wrGETURL.GetResponse();
                HttpStatusCode statusCode = webResponse.StatusCode;
                responseCode = (int)statusCode;
                if (statusCode != HttpStatusCode.NotFound)
                {
                    objStream = wrGETURL.GetResponse().GetResponseStream();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    responseCode = (int)resp.StatusCode;
                    throw new Exception("ERROR: Response code was " + resp.StatusCode.ToString() + " for URL " + sURL, ex);
                    
                }
                else
                {
                    throw new Exception("ERROR: Unexpected error happenned when getting response", ex);
                }
            }

            return objStream;
        }

        static protected string streamToString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            return text;
        }

        public string getAsString(string sURL)
        {
            int responseCode;
            string sData = null;
            string filePath = null;
            HttpMethod method = HttpMethod.GET;
            Stream httpResponseStream = httpRequestStream( method, sURL, sData, out responseCode, filePath);
            string result = streamToString(httpResponseStream);
            httpResponseStream.Close();
            return result;
        }

        static protected string objectArrayToJsonString(HttpMethod method, OARestNounObject[] objectArray)
        {
            string separator = "";
            string json = "[";
            foreach (OARestNounObject objectNoun in objectArray)
            {
                json += separator + objectNoun.ToJson(method);
                separator = ",";
            }
            return json + "]";

        }

        static protected T[] jsonStreamToObjectArray<T>(Stream httpStream)
        {
            T[] resultObjArray = null;

            using (StreamReader streamReader = new StreamReader(httpStream))
            using (JsonReader reader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                resultObjArray = serializer.Deserialize<T[]>(reader);
            }
            return resultObjArray;
        }

        protected T[] getGeneric<T>(string sURL, out int responseCode)
        {
            string sData = null;
            string filePath = null;
            HttpMethod method = HttpMethod.GET;
            Stream httpResponseStream = httpRequestStream( method, sURL, sData, out responseCode, filePath);
            T[] result = jsonStreamToObjectArray<T>(httpResponseStream);
            httpResponseStream.Close();
            return result;
        }

        protected T[] putGeneric<T>(string sURL, OARestNounObject[] objectArray, out int responseCode)
        {
            string filePath = null;
            HttpMethod method = HttpMethod.PUT;
            string sData = objectArrayToJsonString(method, objectArray);
            Stream httpResponseStream = httpRequestStream(method, sURL, sData, out responseCode, filePath);
            T[] result = jsonStreamToObjectArray<T>(httpResponseStream);
            httpResponseStream.Close();
            return result;
        }

        protected virtual PostResponse[] postGeneric(string sURL, OARestNounObject[] objectArray, out int responseCode, string filePath)
        {
            string sData = null;
            HttpMethod method = HttpMethod.POST;
            if (filePath == null)
            {
                sData = objectArrayToJsonString(method, objectArray);
            }
            else
            {
                sData = ((dynamic)objectArray[0]).ToJson(method);
            }
            Stream httpResponseStream = httpRequestStream(method, sURL, sData, out responseCode, filePath);
            PostResponse[] result = jsonStreamToObjectArray<PostResponse>(httpResponseStream);
            httpResponseStream.Close();
            return result;
        }

        protected int deleteGeneric(string sURL, out int responseCode)
        {
            string sData = null;
            string filePath = null;
            HttpMethod method = HttpMethod.DELETE;
            Stream httpResponseStream = httpRequestStream(method, sURL, sData, out responseCode, filePath);
            httpResponseStream.Close();
            return responseCode;
        }
    }
}
