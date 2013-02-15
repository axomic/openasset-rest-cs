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
        static protected Stream httpGetResponseStream(string sURL)
        {
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            // the value of the code should be returned and an argument should be set by reference with the stream TODO
            return objStream;
        }

        static protected string streamToString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            return text;
        }

        static protected string getAsString(string sURL)
        {
            Stream httpResponseStream = httpGetResponseStream(sURL);
            string result = streamToString(httpResponseStream);
            return result;
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

        static protected T[] getGeneric<T>(string sURL)
        {
            Stream httpResponseStream = httpGetResponseStream(sURL);
            return jsonStreamToObjectArray<T>(httpResponseStream);
        }
    }
}
