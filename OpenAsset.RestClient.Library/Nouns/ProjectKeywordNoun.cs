using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library.NounObject;

namespace OpenAsset.RestClient.Library
{
    public class ProjectKeywordNoun : RestAPI<ProjectKeywordObject>
    {

        public ProjectKeywordNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += PROJECT_KEYWORDS_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name, long projectKeywordCategoryId)
        {
            ProjectKeywordObject newNounObj = new ProjectKeywordObject(name,projectKeywordCategoryId);
            ProjectKeywordObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
