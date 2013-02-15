using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OARestClientLib;

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
            RestAPI restAPI = new RestAPI(baseURL,"admin","admin");
            /*
            string sURL = "http://192.168.1.128/REST/Categories?username=admin&password=admin&limit=50&offset=0&ids=3,4";
            string jsonString = OARestClientLib.RequestMethods.getAsString(sURL);
            
            CategoryNoun[] categories = OARestClientLib.RequestMethods.getCategories(sURL);

            string filesURL = "http://192.168.1.128/REST/Files?username=admin&password=admin&limit=10&offset=950&fields=27,33,18&keywords=all&sizes=all";
            FileNoun[] files = OARestClientLib.RequestMethods.getFiles(filesURL);

            string fieldValuesURL = "http://192.168.1.128/REST/Files/3028/Fields?username=admin&password=admin&limit=100&offset=0";
            FieldValueNoun[] fieldValues = OARestClientLib.RequestMethods.getFieldValues(fieldValuesURL);

            string projectsURL = "http://192.168.1.128/REST/Projects/100?username=admin&password=admin&fields=all&projectKeywords=all";
            ProjectNoun[] projects = OARestClientLib.RequestMethods.getProjects(projectsURL);

            string copyrightHoldersURL = "http://192.168.1.128/REST/CopyrightHolders?username=admin&password=admin";
            CopyrightHolderNoun[] copyrightHolders = OARestClientLib.RequestMethods.getCopyrightHolders(copyrightHoldersURL);

            string copyrightPoliciesURL = "http://192.168.1.128/REST/CopyrightPolicies?username=admin&password=admin";
            CopyrightPolicyNoun[] copyrightPolicies = OARestClientLib.RequestMethods.getCopyrightPolicies(copyrightPoliciesURL);

            string fieldsURL = "http://192.168.1.128/REST/Fields?username=admin&password=admin";
            FieldNoun[] fields = OARestClientLib.RequestMethods.getFields(fieldsURL);

            string keywordCategoriesURL = "http://192.168.1.128/REST/KeywordCategories?username=admin&password=admin";
            KeywordCategoryNoun[] keywordCategories = OARestClientLib.RequestMethods.getKeywordCategories(keywordCategoriesURL);

            string keywordsURL = "http://192.168.1.128/REST/Keywords?username=admin&password=admin";
            KeywordNoun[] keywords = OARestClientLib.RequestMethods.getKeywords(keywordsURL);

            string photographersURL = "http://192.168.1.128/REST/Photographers?username=admin&password=admin";
            PhotographerNoun[] photographers = OARestClientLib.RequestMethods.getPhotographers(photographersURL);

            string projectKeywordCategoriesURL = "http://192.168.1.128/REST/ProjectKeywordCategories?username=admin&password=admin";
            ProjectKeywordCategoryNoun[] projectKeywordCategories = OARestClientLib.RequestMethods.getProjectKeywordCategories(projectKeywordCategoriesURL);

            string projectKeywordsURL = "http://192.168.1.128/REST/ProjectKeywords?username=admin&password=admin";
            ProjectKeywordNoun[] projectKeywords = OARestClientLib.RequestMethods.getProjectKeywords(projectKeywordsURL);

            string sizesURL = "http://192.168.1.128/REST/Sizes?username=admin&password=admin";
            SizeNoun[] sizes = OARestClientLib.RequestMethods.getSizes(sizesURL);

            testForm.addToRows<FileNoun>(files);
            testForm.setTexboxText(jsonString);
            */
            Application.Run(testForm);
        }
    }
}
