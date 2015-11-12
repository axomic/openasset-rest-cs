using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using OpenAsset.RestClient.Library;
using OpenAsset.RestClient.Library.Noun;
using OpenAsset.RestClient.Library.Noun.Base;
using OpenAsset.RestClient;

namespace OpenAsset.RestClient.TestLibrary
{
    class BaseTest
    {
        Connection conn;
        public string username;
        public string password;
        public string test_id;
        public bool superUser;

        string _oaURL;
        public string oaURL
        {
            get { return _oaURL; }
            set
            {
                if (!IsUrlValid(value))
                    throw new Exception("URL format not supported");
                _oaURL = value;
            }
        }

        [STAThread]
        public static void Main()
        {
            string oaURL = "http://192.168.4.84";
            string username = "admin";
            string password = "admin";
            bool deleteObjects = true;

            BaseTest test = new BaseTest(oaURL, username, password);

            try
            {
                Console.WriteLine("Test " + test.test_id + " Begin:\n\n");
                test.init();
                Console.WriteLine("\n\nTest " + test.test_id + " End");
            }
            catch (RESTAPIException e)
            {
                System.Console.WriteLine(e);
                System.Console.WriteLine("Exception in the test program: \n\t" + e.ErrorObj);
            }
            Console.ReadLine();
        }

        // Default constructor
        public BaseTest()
        {
            this.oaURL = "";
            this.username = "";
            this.password = "";
            this.test_id = Guid.NewGuid().ToString();
        }

        // Overloaded constructor
        public BaseTest(string oaURL = "", string username = "", string password = "")
        {
            this.oaURL = oaURL;
            this.username = username;
            this.password = password;
            this.superUser = (this.username == "axomic" || this.username == "superuser") ? true : false;
            this.test_id = Guid.NewGuid().ToString();

            if (!EstablishConnection())
            {
                throw new Exception("Incorrect credentials!");
            }
        }

        // Makes a connection to OpenAsset
        public bool EstablishConnection()
        {
            this.conn = Connection.GetConnection(this.oaURL, this.username, this.password);
            return conn.ValidateCredentials();
        }

        // Initiates the test
        public void init()
        {
            RESTOptions<Project> pOptions = new RESTOptions<Project>();
            List<Project> emProjects = this.conn.GetObjects<Project>(5, BaseNoun.GetNoun(typeof(Employee)), pOptions);
            pOptions.SetSearchParameter("fields", "all");
            Project project = this.conn.GetObject<Project>(1, pOptions);
            RESTOptions<Employee> eOptions = new RESTOptions<Employee>();
            Employee employee = this.conn.GetObject<Employee>(5, eOptions);
            //List<Employee> employees = this.conn.GetObjects<Employee>(options);
            //List<Category> categories1 = this.conn.GetObjects<Category>(options);
            //List<Category> categories2 = this.conn.GetObjects<Category>(options);
            System.Console.WriteLine("Recovered employee");
        }

        // Validates a url schema
        private bool IsUrlValid(string url)
        {
            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
    }
}
