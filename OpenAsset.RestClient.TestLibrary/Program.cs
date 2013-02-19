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

            /*
            FileNoun apiMethods = new FileNoun(baseURL, username, password);

            int[] fieldIds = {31,222};
            int[] projectKeywordIds = { 221, 222 };
            FileObject[] resultArray = apiMethods.getNounObjects(projectKeywordIds, fieldIds);

            testForm.addToRows<FileObject>(resultArray);
             */

            ProjectNoun apiMethods = new ProjectNoun(baseURL, username, password);

            int[] fieldIds = { 31, 222 };
            int[] projectKeywordIds = { /*221, 222*/ 1 };
            ProjectObject[] resultArray = apiMethods.getNounObjects();
            resultArray[0].Fields[0].Values[0] = "LIVE FROM THE REST CLIENT LIBRARY!";
            resultArray = apiMethods.putNounObjects(resultArray);

            testForm.addToRows<ProjectObject>(resultArray);

            Application.Run(testForm);
        }
    }
}
