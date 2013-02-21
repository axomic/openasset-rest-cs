using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class ProjectKeywordCategoryNoun : RestAPI<ProjectKeywordCategoryObject>
    {

        public ProjectKeywordCategoryNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += PROJECT_KEYWORD_CATEGORIES_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name)
        {
            ProjectKeywordCategoryObject newNounObj = new ProjectKeywordCategoryObject(name);
            ProjectKeywordCategoryObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
