using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib
{
    public class RestAPI : RequestMethods
    {
        public const string REST_ENDPONT                        = "/REST/";
        public const string CATEGORIES_ENDPOINT                 = "Categories";
        public const string COPYRIGHT_HOLDERS_ENDPOINT          = "CopyrightHolders";
        public const string COPYRIGHT_POLICIES_ENDPOINT         = "CopyrightPolicies";
        public const string FIELDS_ENDPOINT                     = "Fields";
        public const string FILES_ENDPOINT                      = "Files";
        public const string KEYWORD_CATEGORIES_ENDPOINT         = "KeywordCategories";
        public const string KEYWORDS_ENDPOINT                   = "Keywords";
        public const string PROJECT_KEYWORD_CATEGORIES_ENDPOINT = "ProjectKeywordCategories";
        public const string PROJECT_KEYWORDS_ENDPOINT           = "ProjectKeywords";
        public const string PROJECTS_ENDPOINT                   = "Projects";
        public const string SIZES_ENDPOINT                      = "Sizes";

        private string _baseURL;
        private string _password;
        private string _username;

        public RestAPI(string baseURL)
        {
            // if the user is already logged in for any other reason
            init(baseURL, null, null);
        }

        public RestAPI(string baseURL, string username, string password)
        {
            init(baseURL,username,password);
        }

        private void init(string baseURL, string username, string password)
        {
            _baseURL = baseURL;
            _username = username;
            _password = password;
        }

        

        // get nouns
        public CategoryNoun[] getCategories(string sURL)
        {
            return getGeneric<CategoryNoun>(sURL);
        }

        public FileNoun[] getFiles(string sURL)
        {
            return getGeneric<FileNoun>(sURL);
        }

        public FieldValueNoun[] getFieldValues(string sURL)
        {
            return getGeneric<FieldValueNoun>(sURL);
        }

        public KeywordValueNoun[] getKeywordValues(string sURL)
        {
            return getGeneric<KeywordValueNoun>(sURL);
        }

        public SizeValueNoun[] getSizeValues(string sURL)
        {
            return getGeneric<SizeValueNoun>(sURL);
        }

        public ProjectKeywordValueNoun[] getProjectKeywordValues(string sURL)
        {
            return getGeneric<ProjectKeywordValueNoun>(sURL);
        }

        public ProjectNoun[] getProjects(string sURL)
        {
            return getGeneric<ProjectNoun>(sURL);
        }

        public CopyrightHolderNoun[] getCopyrightHolders(string sURL)
        {
            return getGeneric<CopyrightHolderNoun>(sURL);
        }

        public CopyrightPolicyNoun[] getCopyrightPolicies(string sURL)
        {
            return getGeneric<CopyrightPolicyNoun>(sURL);
        }

        public FieldNoun[] getFields(string sURL)
        {
            return getGeneric<FieldNoun>(sURL);
        }

        public KeywordCategoryNoun[] getKeywordCategories(string sURL)
        {
            return getGeneric<KeywordCategoryNoun>(sURL);
        }

        public KeywordNoun[] getKeywords(string sURL)
        {
            return getGeneric<KeywordNoun>(sURL);
        }

        public PhotographerNoun[] getPhotographers(string sURL)
        {
            return getGeneric<PhotographerNoun>(sURL);
        }

        public ProjectKeywordCategoryNoun[] getProjectKeywordCategories(string sURL)
        {
            return getGeneric<ProjectKeywordCategoryNoun>(sURL);
        }

        public ProjectKeywordNoun[] getProjectKeywords(string sURL)
        {
            return getGeneric<ProjectKeywordNoun>(sURL);
        }

        public SizeNoun[] getSizes(string sURL)
        {
            return getGeneric<SizeNoun>(sURL);
        }
    }
}
