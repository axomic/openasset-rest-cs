using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class ProjectNoun : RestAPI<ProjectObject>
    {

        public ProjectNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += PROJECTS_ENDPOINT;
        }

        // public methods
        public ProjectObject[] getNounObjects( int[] keywords = null, int[] fields = null,
                                               int limit = 10, int offset = 0, bool forceHTTPRequest = _forceRequestDefault)
        {
            string separator = ",";
            string resultURL = _nounURL;
            resultURL = addParameter(resultURL, LIMIT_PARAMETER, limit.ToString());
            resultURL = addParameter(resultURL, OFFSET_PARAMETER, offset.ToString());
            if (keywords == null)
            {
                resultURL = addParameter(resultURL, PROJECT_KEYWORDS_PARAMETER, "all");
            }
            else
            {
                string keywordsStr = String.Join(separator, new List<int>(keywords).ConvertAll(i => i.ToString()).ToArray());
                resultURL = addParameter(resultURL, PROJECT_KEYWORDS_PARAMETER, keywordsStr);
            }
            if (fields == null)
            {
                resultURL = addParameter(resultURL, FIELDS_PARAMETER, "all");
            }
            else
            {
                string fieldsStr = String.Join(separator, new List<int>(fields).ConvertAll(i => i.ToString()).ToArray());
                resultURL = addParameter(resultURL, FIELDS_PARAMETER, fieldsStr);
            }
            return getNounObjectArray(resultURL, forceHTTPRequest);
        }

        public PostResponse createNewObjectNoun( string name, string code)
        {
            ProjectObject newNounObj = new ProjectObject(name, code);
            ProjectObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
