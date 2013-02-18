using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.Noun;

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

        public const string SIZES_PARAMETER             = "sizes";
        public const string FIELDS_PARAMETER            = "fields";
        public const string KEYWORDS_PARAMETER          = "keywords";
        public const string PROJECT_KEYWORDS_PARAMETER  = "projectKeywords";
        public const string LIMIT_PARAMETER             = "limit";
        public const string OFFSET_PARAMETER            = "offset";

        private string _baseURL;

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
        public CategoryObject[] getCategories(string sURL)
        {
            return getGeneric<CategoryObject>(sURL);
        }

        public FileObject[] getFiles(string sURL)
        {
            return getGeneric<FileObject>(sURL);
        }

        public FieldValueObject[] getFieldValues(string sURL)
        {
            return getGeneric<FieldValueObject>(sURL);
        }

        public KeywordValueObject[] getKeywordValues(string sURL)
        {
            return getGeneric<KeywordValueObject>(sURL);
        }

        public SizeValueObject[] getSizeValues(string sURL)
        {
            return getGeneric<SizeValueObject>(sURL);
        }

        public ProjectKeywordValueObject[] getProjectKeywordValues(string sURL)
        {
            return getGeneric<ProjectKeywordValueObject>(sURL);
        }

        public ProjectObject[] getProjects(string sURL)
        {
            return getGeneric<ProjectObject>(sURL);
        }

        public CopyrightHolderObject[] getCopyrightHolders(string sURL)
        {
            return getGeneric<CopyrightHolderObject>(sURL);
        }

        public CopyrightPolicyObject[] getCopyrightPolicies(string sURL)
        {
            return getGeneric<CopyrightPolicyObject>(sURL);
        }

        public FieldObject[] getFields(string sURL)
        {
            return getGeneric<FieldObject>(sURL);
        }

        public KeywordCategoryObject[] getKeywordCategories(string sURL)
        {
            return getGeneric<KeywordCategoryObject>(sURL);
        }

        public KeywordObject[] getKeywords(string sURL)
        {
            return getGeneric<KeywordObject>(sURL);
        }

        public PhotographerObject[] getPhotographers(string sURL)
        {
            return getGeneric<PhotographerObject>(sURL);
        }

        public ProjectKeywordCategoryObject[] getProjectKeywordCategories(string sURL)
        {
            return getGeneric<ProjectKeywordCategoryObject>(sURL);
        }

        public ProjectKeywordObject[] getProjectKeywords(string sURL)
        {
            return getGeneric<ProjectKeywordObject>(sURL);
        }

        public SizeObject[] getSizes(string sURL)
        {
            return getGeneric<SizeObject>(sURL);
        }
    }
}
