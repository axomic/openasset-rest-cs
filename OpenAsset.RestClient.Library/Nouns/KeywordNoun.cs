using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class KeywordNoun : RestAPI<KeywordObject>
    {

        public KeywordNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += KEYWORDS_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name, long keywordCategoryId)
        {
            KeywordObject newNounObj = new KeywordObject(name, keywordCategoryId);
            KeywordObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
