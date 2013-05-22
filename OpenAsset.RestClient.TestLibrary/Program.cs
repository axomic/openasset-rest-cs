using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenAsset.RestClient.Library;
using OpenAsset.RestClient.Library.NounObject;

namespace OpenAsset.RestClient.TestLibrary
{
    static class Program
    {
        static private FileNoun _apiMethods;
        static public Form1 _testForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string baseURL = "http://192.168.1.153";
            string password = "admin";
            string username = "admin";

            _apiMethods = new FileNoun(baseURL, username, password);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _testForm = new Form1();
/*

            int[] fieldIds = { 31, 222 };
            int[] projectKeywordIds = { 1 };

            //get file rows as FileObject
            FileObject[] resultArray = apiMethods.getNounObjects();
            //
            FileObject resultObj = apiMethods.getNounObjectById(7390);

            //updating a file rows
            resultArray[5].Caption = "new caption";
            //resultArray[5].Fields[0].Values = new string{"field1", "field2"};
            int putResponseCode = apiMethods.putNounObjects(resultArray);

            //add a file row to the DB
            string filepath = "C:\\Users\\Public\\Pictures\\Sample Pictures\\Koala.jpg";
            PostResponse postResp = apiMethods.createNewObjectNoun("TEST TODAY", 1, 92, 0, 3, true, "TEST AGAIN", 1, "description", 1, filepath);

            //int deleteResponseCode = apiMethods.deleteNounObjectById(7390);

            testForm.addToRows<FileObject>(resultArray);
*/

            Application.Run(_testForm);
        }

        public static void fillGetData(int limit, int offset)
        {
            FileObject[] resultArray = _apiMethods.getNounObjects(limit: limit, offset: offset,forceHTTPRequest:true);
            _testForm.addToRows<FileObject>(resultArray);
        }

        public static void updatePutData(long id, string caption)
        {
            FileObject resultObj = _apiMethods.getNounObjectById(id);
            resultObj.Caption = caption;
            int putResponseCode = _apiMethods.putNounObjects(new FileObject[] {resultObj});
            System.Windows.Forms.MessageBox.Show("Response code : " + putResponseCode);
        }

        public static void newPostData(string filepath, int accessLevel, long categoryId)
        {
            PostResponse postResp = _apiMethods.createNewObjectNoun( filepath, accessLevel, categoryId);
            System.Windows.Forms.MessageBox.Show(postResp.Message + "\nNew Id : " + postResp.NewId);
        }


        public static void delete(long id)
        {
            _apiMethods.deleteNounObjectById(id);
        }
    }
}
