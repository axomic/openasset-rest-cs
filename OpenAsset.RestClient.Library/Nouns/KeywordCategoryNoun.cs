using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library.NounObject;

namespace OpenAsset.RestClient.Library
{
    public class KeywordCategoryNoun : RestAPI<KeywordCategoryObject>
    {

        public KeywordCategoryNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += KEYWORD_CATEGORIES_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name, long categoryId)
        {
            KeywordCategoryObject newNounObj = new KeywordCategoryObject(name, categoryId);
            KeywordCategoryObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
