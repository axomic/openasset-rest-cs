using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class CategoryNoun : RestAPI<CategoryObject>
    {

        public CategoryNoun(string baseURL, string username, string password)
            : base(baseURL,username,password)
        {
            _nounURL += CATEGORIES_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name, bool isProjectCategory)
        {
            CategoryObject newNounObj = new CategoryObject(name, isProjectCategory);
            CategoryObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
