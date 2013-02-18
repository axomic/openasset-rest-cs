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

            string baseURL = "http://192.168.1.128";
            string password = "admin";
            string username = "admin";

            ProjectNoun apiMethods = new ProjectNoun(baseURL, username, password);
            ProjectObject[] resultArray = apiMethods.getNounObjects();

            testForm.addToRows<ProjectObject>(resultArray);

            Application.Run(testForm);
        }
    }
}
