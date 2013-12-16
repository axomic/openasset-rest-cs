using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenAsset.RestClient.Library;
using OpenAsset.RestClient.Library.Noun;
using OpenAsset.RestClient;

namespace OpenAsset.RestClient.TestLibrary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string oaURL = "http://192.168.1.142";
            string username = "axomic";
            string password = "***REMOVED***";
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            System.Console.WriteLine("TEST RUNNING!");
            ConnectionFactory.Instance.testMethod();

            ConnectionHelper connectionHelper = ConnectionFactory.Instance.getConnectionHelper(oaURL, username, password);
            try
            {
                bool validUser = connectionHelper.ValidateCredentials();
                RESTOptions options = new RESTOptions();
                options.SetSearchParameter("sizes","all");

                Size size = connectionHelper.GetObject<Size>(1, options);
                List<Size> sizeList = connectionHelper.GetObjects<Size>(957, "Files", options);
                File file = connectionHelper.GetObject<File>(957, options);
                Category category = connectionHelper.GetObject<Category>(5, options);
                category.name = "restAPI_test";
                connectionHelper.SendObject<Category>(category,true);
                //List<File> size = connectionHelper.GetObjects<File>(new RESTOptions());
            }
            catch (RESTAPIException e)
            {
                System.Console.WriteLine(e);
                System.Console.WriteLine("Exception in the test program: \n\t" + e.ErrorObj);
            }
        }
    }
}
