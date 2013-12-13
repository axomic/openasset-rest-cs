using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    public class ConnectionFactory
    {
        private static ConnectionFactory _instance;

        public ConnectionFactory() { }

        public static ConnectionFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConnectionFactory();
                }
                return _instance;
            }
        }

        public ConnectionHelper getConnectionHelper(string oaURL, string username = null, string password = null){
            ConnectionHelper result = null;
            if (username == null && password == null)
            {
                result = new ConnectionHelper(oaURL);
            }
            else
            {
                result = new ConnectionHelper(oaURL,username,password);
            }
            return result;

        }

        public void testMethod(){
            System.Console.WriteLine("RestAPI test method()");
        }
    }
}
