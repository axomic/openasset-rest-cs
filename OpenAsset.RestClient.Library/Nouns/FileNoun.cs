using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library.NounObject;

namespace OpenAsset.RestClient.Library
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
                                               int limit = 10, int offset = 0, bool forceHTTPRequest = _forceRequestDefault)
        {
            string separator = ",";
            string resultURL = _nounURL;
            resultURL = addParameter(resultURL, LIMIT_PARAMETER, limit.ToString());
            resultURL = addParameter(resultURL, OFFSET_PARAMETER, offset.ToString());
            if (keywords == null)
            {
                resultURL = addParameter(resultURL, KEYWORDS_PARAMETER, "all");
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

        public PostResponse createNewObjectNoun(long categoryId, long projectId, int accessLevel, string newFilePath)
        {
            // need to make it so that it works with default values of the REST API or sends a Json with only the mandatory fields
            return createNewObjectNoun(null, categoryId, 0, 0, accessLevel, true, null, 0, null, 0, newFilePath);
        }


        public PostResponse createNewObjectNoun(string filepath, int accessLevel, long categoryId)
        {
            FileObject newFileObj = new FileObject( filepath, accessLevel, categoryId);
            FileObject[] newFileObjArray = { newFileObj };
            PostResponse[] response = this.postNounObjects(newFileObjArray, filePath: filepath);
            return response[0];
        }

        public PostResponse createNewObjectNoun(string name, long categoryId, long projectId, long albumId,
    int accessLevel, bool alive, string caption, long copyrightHolderId, string description, long photographerId, string newFilePath)
        {
            FileObject newFileObj = new FileObject(name, categoryId, projectId, albumId, accessLevel, alive, caption, copyrightHolderId, description, photographerId);
            FileObject[] newFileObjArray = { newFileObj };
            PostResponse[] response = this.postNounObjects(newFileObjArray, filePath: newFilePath);
            return response[0];
        }
    }
}
;