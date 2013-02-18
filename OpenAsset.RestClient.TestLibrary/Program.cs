using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OARestClientLib;
using OARestClientLib.Noun;

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
            
            string sURL = "http://192.168.1.128/REST/Categories?username=admin&password=admin&limit=50&offset=0&ids=3,4";
            string jsonString = restAPI.getAsString(sURL);
            
            CategoryObject[] categories = restAPI.getCategories(sURL);

            string filesURL = "http://192.168.1.128/REST/Files?username=admin&password=admin&limit=100&offset=950&fields=27,33,18&keywords=all&sizes=all";
            FileObject[] files = restAPI.getFiles(filesURL);

            string fieldValuesURL = "http://192.168.1.128/REST/Files/3028/Fields?username=admin&password=admin&limit=100&offset=0";
            FieldValueObject[] fieldValues = restAPI.getFieldValues(fieldValuesURL);

            string projectsURL = "http://192.168.1.128/REST/Projects/100?username=admin&password=admin&fields=all&projectKeywords=all";
            ProjectObject[] projects = restAPI.getProjects(projectsURL);

            string copyrightHoldersURL = "http://192.168.1.128/REST/CopyrightHolders?username=admin&password=admin";
            CopyrightHolderObject[] copyrightHolders = restAPI.getCopyrightHolders(copyrightHoldersURL);

            string copyrightPoliciesURL = "http://192.168.1.128/REST/CopyrightPolicies?username=admin&password=admin";
            CopyrightPolicyObject[] copyrightPolicies = restAPI.getCopyrightPolicies(copyrightPoliciesURL);

            string fieldsURL = "http://192.168.1.128/REST/Fields?username=admin&password=admin";
            FieldObject[] fields = restAPI.getFields(fieldsURL);

            string keywordCategoriesURL = "http://192.168.1.128/REST/KeywordCategories?username=admin&password=admin";
            KeywordCategoryObject[] keywordCategories = restAPI.getKeywordCategories(keywordCategoriesURL);

            string keywordsURL = "http://192.168.1.128/REST/Keywords?username=admin&password=admin";
            KeywordObject[] keywords = restAPI.getKeywords(keywordsURL);

            string photographersURL = "http://192.168.1.128/REST/Photographers?username=admin&password=admin";
            PhotographerObject[] photographers = restAPI.getPhotographers(photographersURL);

            string projectKeywordCategoriesURL = "http://192.168.1.128/REST/ProjectKeywordCategories?username=admin&password=admin";
            ProjectKeywordCategoryObject[] projectKeywordCategories = restAPI.getProjectKeywordCategories(projectKeywordCategoriesURL);

            string projectKeywordsURL = "http://192.168.1.128/REST/ProjectKeywords?username=admin&password=admin";
            ProjectKeywordObject[] projectKeywords = restAPI.getProjectKeywords(projectKeywordsURL);

            string sizesURL = "http://192.168.1.128/REST/Sizes?username=admin&password=admin";
            SizeObject[] sizes = restAPI.getSizes(sizesURL);

            testForm.addToRows<FileObject>(files);
            testForm.setTexboxText(jsonString);
            
            Application.Run(testForm);
        }
    }
}
