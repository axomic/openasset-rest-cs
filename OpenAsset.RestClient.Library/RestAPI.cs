using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public abstract class RestAPI<T> : RequestMethods
    {
        protected const string REST_ENDPOINT                        = "/REST/";
        protected const string CATEGORIES_ENDPOINT                  = "Categories";
        protected const string COPYRIGHT_HOLDERS_ENDPOINT           = "CopyrightHolders";
        protected const string COPYRIGHT_POLICIES_ENDPOINT          = "CopyrightPolicies";
        protected const string FIELDS_ENDPOINT                      = "Fields";
        protected const string FILES_ENDPOINT                       = "Files";
        protected const string KEYWORD_CATEGORIES_ENDPOINT          = "KeywordCategories";
        protected const string KEYWORDS_ENDPOINT                    = "Keywords";
        protected const string PROJECT_KEYWORD_CATEGORIES_ENDPOINT  = "ProjectKeywordCategories";
        protected const string PROJECT_KEYWORDS_ENDPOINT            = "ProjectKeywords";
        protected const string PROJECTS_ENDPOINT                    = "Projects";
        protected const string SIZES_ENDPOINT                       = "Sizes";
        protected const string PHOTOGRAPHERS_ENDPOINT               = "Photographers";

        protected const string SIZES_PARAMETER              = "sizes";
        protected const string FIELDS_PARAMETER             = "fields";
        protected const string KEYWORDS_PARAMETER           = "keywords";
        protected const string PROJECT_KEYWORDS_PARAMETER   = "projectKeywords";
        protected const string LIMIT_PARAMETER              = "limit";
        protected const string OFFSET_PARAMETER             = "offset";

        protected const bool _forceRequest = true;

        protected string _baseURL;
        protected string _nounURL;
        protected T[] _cachedNounObjectArray;

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
            _nounURL = baseURL+REST_ENDPOINT;
            _username = username;
            _password = password;
            _cachedNounObjectArray = null;
        }

        protected string addParameter(string sURL, string parameterName, string parameterValue)
        {
            if (!sURL.Contains("?"))
            {
                sURL += "?";
            }
            else
            {
                sURL += "&";
            }
            sURL += parameterName + "=" + parameterValue;
            return sURL;
        }

        // get nouns
        protected T[] getNounObjectArray(string sURL, bool forceHTTPRequest = _forceRequest)
        {
            int responseCode;
            if (forceHTTPRequest || _cachedNounObjectArray == null)
            {
                _cachedNounObjectArray = getGeneric<T>(sURL, out responseCode);
            }
            return _cachedNounObjectArray;
        }

        // public methods
        public virtual T[] getNounObjects(int limit = 10, int offset = 0, bool forceHTTPRequest = _forceRequest)
        {
            string resultURL = _nounURL;
            resultURL = addParameter(resultURL, LIMIT_PARAMETER, limit.ToString());
            resultURL = addParameter(resultURL, OFFSET_PARAMETER, offset.ToString());
            return getNounObjectArray(resultURL, forceHTTPRequest);
        }

        public virtual T getNounObjectById(long id, int limit = 10, int offset = 0, bool forceHTTPRequest = _forceRequest)
        {
            string resultURL = _nounURL;
            resultURL = addParameter(resultURL, LIMIT_PARAMETER, limit.ToString());
            resultURL = addParameter(resultURL, OFFSET_PARAMETER, offset.ToString());
            T[] resultObj = getNounObjectArray(resultURL.Replace("?", "/" + id + "?"), forceHTTPRequest);
            if (resultObj.Length == 1)
                return resultObj[0];
            else
                return default(T);
        }

        public virtual int putNounObjects(T[] objectArray, int limit = 10, int offset = 0)
        {
            string resultURL = _nounURL;
            int responseCode;
            resultURL = addParameter(resultURL, LIMIT_PARAMETER, limit.ToString());
            resultURL = addParameter(resultURL, OFFSET_PARAMETER, offset.ToString());
            putGeneric<T>(resultURL, objectArray, out responseCode);
            return responseCode;
        }

        protected virtual PostResponse[] postNounObjects(T[] objectArray, int limit = 10, int offset = 0, string filePath = null)
        {
            string resultURL = _nounURL;
            int responseCode;
            resultURL = addParameter(resultURL, LIMIT_PARAMETER, limit.ToString());
            resultURL = addParameter(resultURL, OFFSET_PARAMETER, offset.ToString());
            PostResponse[] result = postGeneric<T>(resultURL, objectArray, out responseCode, filePath);
            return result;
        }

        public virtual int deleteNounObjects(T[] objectArray)
        {
            string resultURL = _nounURL;
            int responseCode = 0;
            foreach (dynamic objectNoun in objectArray)
            {
                deleteGeneric(resultURL.Replace("?", "/" + objectNoun.Id + "?"), out responseCode);
            }
            return responseCode;
        }
    }
}
