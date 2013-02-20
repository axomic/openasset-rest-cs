using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class FileNoun : RestAPI<FileObject>
    {

        public FileNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += FILES_ENDPOINT;
        }

        // public methods
        public FileObject[] getNounObjects(int[] keywords = null, int[] fields = null, int[] sizes = null,
                                               int limit = 10, int offset = 0, bool forceHTTPRequest = _forceRequest)
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
                resultURL = addParameter(resultURL, KEYWORDS_PARAMETER, keywordsStr);
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
            if (sizes == null)
            {
                resultURL = addParameter(resultURL, SIZES_PARAMETER, "all");
            }
            else
            {
                string sizesStr = String.Join(separator, new List<int>(sizes).ConvertAll(i => i.ToString()).ToArray());
                resultURL = addParameter(resultURL, SIZES_PARAMETER, sizesStr);
            }
            
            return getNounObjectArray(resultURL, forceHTTPRequest);
        }
    }
}
