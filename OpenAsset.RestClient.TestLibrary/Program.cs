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

            ConnectionHelper connectionHelper = ConnectionHelper.GetConnectionHelper(oaURL, username, password);
            try
            {
                bool validUser = connectionHelper.ValidateCredentials();
                RESTOptions<File> optionsFile = new RESTOptions<File>();
                optionsFile.SetSearchParameter("sizes", "all");
                optionsFile.AddDisplayField("access_level");
                optionsFile.AddDisplayField("caption");
                optionsFile.AddDisplayField("category_id");
                optionsFile.AddOrderBy("id");
                bool result = optionsFile.RemoveDisplayField("category_id");
                File file = connectionHelper.GetObject<File>(957, optionsFile);
                file.Replace(file);

                RESTOptions<Size> optionsSize = new RESTOptions<Size>();
                Size size = connectionHelper.GetObject<Size>(1, optionsSize);
                List<Size> sizeList = connectionHelper.GetObjects<Size>(957, "Files", optionsSize);

                RESTOptions<Category> optionsCategory = new RESTOptions<Category>();
                Category category = connectionHelper.GetObject<Category>(5, optionsCategory);

                RESTOptions<Album> optionsAlbum = new RESTOptions<Album>();
                Album album = connectionHelper.GetObject<Album>(53, optionsAlbum);
                DateTime d = album.Updated;

                //post
                //connectionHelper.SendObject<Category>(category,true);
                //put doesn't seem to be implemented
                //connectionHelper.SendObject<Category>(category, false);
            }
            catch (RESTAPIException e)
            {
                System.Console.WriteLine(e);
                System.Console.WriteLine("Exception in the test program: \n\t" + e.ErrorObj);
            }
        }
    }
}
