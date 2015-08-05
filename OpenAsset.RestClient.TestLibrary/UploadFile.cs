using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library;
using OpenAsset.RestClient.Library.Noun;
using OpenAsset.RestClient.Library.Noun.Base;
using OpenAsset.RestClient;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

// Class tests the integration of individual nouns
namespace OpenAsset.RestClient.TestLibrary
{
    class UploadFile
    {
        Connection conn;
        public string username;
        public string password;
        public string filepath;
        public string test_id;
        public string oaURL;

        [STAThread]
        public static void Main(string[] args)
        {
            string oaURL = "https://localhost";
            string username = "admin";
            string password = "admin";
            string basepath = @"C:\working\";

            UploadFile[] tests = new UploadFile[11];
            BackgroundWorker[] bgs = new BackgroundWorker[11];

            for (int i = 0; i < 11; i++)
            {
                string filepath = basepath + "cat" + i + ".jpg";
                tests[i] = new UploadFile(oaURL, username, password, filepath);
                bgs[i] = new BackgroundWorker();
                bgs[i].DoWork += UploadFile_DoWork;
                bgs[i].RunWorkerAsync(tests[i]);
            }


            Console.ReadLine();
        }

        static void UploadFile_DoWork(object sender, DoWorkEventArgs e)
        {
            //Thread.Sleep(new Random().Next(100));
            UploadFile test = e.Argument as UploadFile;
            try
            {
                Console.WriteLine("Uploading " + test.filepath + " to " + test.oaURL + ":\n\n");
                test.init();
                Console.WriteLine("\n\nDone");
            }
            catch (RESTAPIException ex)
            {
                //System.Console.WriteLine(ex);
                System.Console.WriteLine("Exception in the program: \n\t" + ex.ErrorObj);
            }
        }

        // Default constructor
        public UploadFile()
        {
            this.oaURL = "";
            this.username = "";
            this.password = "";
            this.filepath = "";
            this.test_id = Guid.NewGuid().ToString();
        }

        // Overloaded constructor
        public UploadFile(string oaURL = "", string username = "", string password = "", string filepath = "")
        {
            this.oaURL = oaURL;
            this.username = username;
            this.password = password;
            this.filepath = filepath;
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

        // Generates and uploads a File to OpenAsset, along with tagging it's field, keyword, photographer, access level, and copyright holder
        public void init()
        {
            string filename = System.IO.Path.GetFileName(this.filepath);

            Console.Write("Uploading File: ");
            File fileItem = new File();
            fileItem.Filename = this.filepath;
            fileItem.OriginalFilename = filename;

            fileItem.Caption = this.test_id + "_Test_Caption";
            fileItem.Description = this.test_id + "_Test_Description";

            fileItem.CategoryId = 2;

            File resp = this.conn.SendObject<File>(fileItem, this.filepath, true);
            Console.WriteLine("File ID: " + resp.Id);
        }
    }
}
