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

            ConnectionHelper connectionHelper = ConnectionHelper.GetConnectionHelper(oaURL, username, password);// get a connection helper from the "pool"
            try
            {

                ////check if user is valid
                //bool validUser = connectionHelper.ValidateCredentials();

                //// get
                //RESTOptions<File> optionsFile = new RESTOptions<File>();
                //optionsFile.SetSearchParameter("sizes", "all");
                //optionsFile.AddDisplayField("access_level");
                //optionsFile.AddDisplayField("caption");
                //optionsFile.AddDisplayField("id");
                //optionsFile.AddDisplayField("category_id");
                //optionsFile.AddOrderBy("id");
                //bool removedDisplayField = optionsFile.RemoveDisplayField("category_id");
                //File file = connectionHelper.GetObject<File>(957, optionsFile);
                //file.Replace(file);

                //RESTOptions<Size> optionsSize = new RESTOptions<Size>();
                //Size size = connectionHelper.GetObject<Size>(1, optionsSize);
                //List<Size> sizeList = connectionHelper.GetObjects<Size>(957, "Files", optionsSize);

                //RESTOptions<Category> optionsCategory = new RESTOptions<Category>();
                //Category category = connectionHelper.GetObject<Category>(5, optionsCategory);

                //RESTOptions<Album> optionsAlbum = new RESTOptions<Album>();
                //Album album = connectionHelper.GetObject<Album>(53, optionsAlbum);
                //DateTime d = album.Updated;

                //RESTOptions<Search> optionsSearch = new RESTOptions<Search>();
                //List<Search> searchList = connectionHelper.GetObjects<Search>(optionsSearch);
                //Search search0 = searchList.ElementAt<Search>(0);
                //Search search1 = searchList.ElementAt<Search>(1);

                ////post
                //Search postSearch = new Search();
                //postSearch.Name = "POSTTESTNAME";
                //SearchItem searchItem = new SearchItem();
                //searchItem.Code = "popularFields";
                //searchItem.Values = new List<string>();
                //searchItem.Values.Add("test1");
                //searchItem.Values.Add("test2");
                //searchItem.Values.Add("test3");
                //postSearch.SearchItems = new List<SearchItem>();
                //postSearch.SearchItems.Add(searchItem);
                //Search newSearch = connectionHelper.SendObject<Search>(postSearch, true); // only returns a Search object with only an Id
                //newSearch = connectionHelper.GetObject<Search>(newSearch.Id, optionsSearch); // get new search data

                ////put
                //Search putSearch = new Search();
                //putSearch.Name = "PUTTTESTNAME";
                //SearchItem putSearchItem = new SearchItem();
                //putSearchItem.Code = "popularFields";
                //putSearchItem.Values = new List<string>();
                //putSearchItem.Values.Add("puttest1");
                //putSearchItem.Values.Add("puttest2");
                //putSearchItem.Values.Add("puttest2");
                //putSearch.SearchItems = new List<SearchItem>();
                //putSearch.SearchItems.Add(putSearchItem);
                //putSearch.Id = newSearch.Id;
                //Search modifiedSearch = connectionHelper.SendObject<Search>(putSearch, false);

                //// nested example
                //RESTOptions<Result> optionsResult = new RESTOptions<Result>();
                //List<Result> resultList = connectionHelper.GetObjects<Result>(search0.Id, "Searches", optionsResult);
                //Result result0 = resultList.ElementAt<Result>(0);

                // file upload
                string filename = "C:\\Users\\duarte.aragao\\Downloads\\";
                filename += "test.jpg";
                RESTOptions<File> optionsFileUpload = new RESTOptions<File>();
                File fileUpload = new File();
                fileUpload.CategoryId = 5;
                fileUpload.AccessLevel = 1;
                fileUpload.Rank = 1;
                File newFile = connectionHelper.SendObject<File>(fileUpload, filename);


            }
            catch (RESTAPIException e)
            {
                System.Console.WriteLine(e);
                System.Console.WriteLine("Exception in the test program: \n\t" + e.ErrorObj);
            }
        }
    }
}
