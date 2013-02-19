using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
// could have used the JavaScriptSerializer but the Newtonsoft seems to be faster
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OARestClientLib
{

    public class Test
    {
        public Test()
        {
        }

        static public string getString()
        {
            return "Hello! This is REST Client Lib!";
        }
    }

    abstract public class RequestMethods
    {
        protected string _password;
        protected string _username;

        protected Stream httpGetResponseStream(string sURL, out int responseCode)
        {
            return httpRequestStream( "GET", sURL, null, out responseCode);
        }

        protected Stream httpPutResponseStream(string sURL, string sData, out int responseCode)
        {
            return httpRequestStream("PUT", sURL, sData, out responseCode);
        }

        protected Stream httpRequestStream(string method, string sURL, string sData, out int responseCode)
        {
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            wrGETURL.Method = method;
            if (_username != null && _password != null)
            {
                // set the authorization header
                string credentials = String.Format("{0}:{1}", _username, _password);
                byte[] bytes = Encoding.ASCII.GetBytes(credentials);
                string base64 = Convert.ToBase64String(bytes);
                string authorization = String.Concat("Basic ", base64);
                wrGETURL.Headers.Add("Authorization", authorization);
            }

            if (sData != null)
            {
                // set the raw data
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
                    throw new Exception("ERROR: Response code was " + resp.StatusCode.ToString() + "for URL " + sURL, ex);
                    
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
            Stream httpResponseStream = httpGetResponseStream(sURL, out responseCode);
            string result = streamToString(httpResponseStream);
            httpResponseStream.Close();
            return result;
        }

        static protected string objectArrayToJsonString<T>(T[] objectArray)
        {
            string separator = "";
            string json = "[";
            foreach (dynamic objectNoun in objectArray)
            {
                json += separator + objectNoun.ToJson();
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
            Stream httpResponseStream = httpGetResponseStream(sURL, out responseCode);
            T[] result = jsonStreamToObjectArray<T>(httpResponseStream);
            httpResponseStream.Close();
            return result;
        }

        protected T[] putGeneric<T>(string sURL, T[] objectArray, out int responseCode)
        {
            string sData = objectArrayToJsonString(objectArray);
            Stream httpResponseStream = httpPutResponseStream(sURL, sData, out responseCode);
            T[] result = jsonStreamToObjectArray<T>(httpResponseStream);
            httpResponseStream.Close();
            return result;
        }
    }
}
