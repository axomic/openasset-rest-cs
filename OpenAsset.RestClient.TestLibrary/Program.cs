using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OARestClientLib;
using OARestClientLib.NounObject;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 testForm = new Form1();

            string baseURL = "http://192.168.1.153";
            string password = "admin";
            string username = "admin";

            FileNoun apiMethods = new FileNoun(baseURL, username, password);

            int[] fieldIds = { 31, 222 };
            int[] projectKeywordIds = { /*221, 222*/ 1 };

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

            

            //resultArray[1].Caption = "LIVE FROM THE REST CLIENT LIBRARY!";
            //resultArray[1].Fields[1].Values[0] = "LIVE FROM THE REST CLIENT LIBRARY!";
            //int responseCode = apiMethods.putNounObjects(resultArray);
            //PostResponse[] responseCode = apiMethods.postNounObjects(resultArray);
            //PostResponse[] responseCode = apiMethods.postNounObjects(resultArray,10,0,filepath);
            
            //ProjectObject[] newProjArray = { new ProjectObject("TEST 1", "A0009") };
            //PostResponse[] responseCode = apiMethods.postNounObjects(newProjArray);

            testForm.addToRows<FileObject>(resultArray);

            Application.Run(testForm);
        }
    }
}
