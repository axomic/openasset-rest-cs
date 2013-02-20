using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class ProjectKeywordNoun : RestAPI<ProjectKeywordObject>
    {

        public ProjectKeywordNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += PROJECT_KEYWORDS_ENDPOINT;
        }
    }
}
