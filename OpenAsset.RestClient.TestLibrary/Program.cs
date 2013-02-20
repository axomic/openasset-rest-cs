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
            FileObject[] resultArray = apiMethods.getNounObjects();
            resultArray[5].Name = "new name";
            //resultArray[1].Caption = "LIVE FROM THE REST CLIENT LIBRARY!";
            //resultArray[1].Fields[1].Values[0] = "LIVE FROM THE REST CLIENT LIBRARY!";
            //int responseCode = apiMethods.putNounObjects(resultArray);
            //PostResponse[] responseCode = apiMethods.postNounObjects(resultArray);
            string filepath = "C:\\Users\\Public\\Pictures\\Sample Pictures\\Penguins.jpg";
            PostResponse[] responseCode = apiMethods.postNounObjects(resultArray,10,0,filepath);

            testForm.addToRows<FileObject>(resultArray);

            Application.Run(testForm);
        }
    }
}
