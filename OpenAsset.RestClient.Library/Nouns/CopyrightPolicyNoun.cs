using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class CopyrightPolicyNoun : RestAPI<CopyrightPolicyObject>
    {

        public CopyrightPolicyNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += COPYRIGHT_POLICIES_ENDPOINT;
        }
    }
}
