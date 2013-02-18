using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class KeywordCategoryNoun : RestAPI<KeywordCategoryObject>
    {

        public KeywordCategoryNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += KEYWORD_CATEGORIES_ENDPOINT;
        }
    }
}
