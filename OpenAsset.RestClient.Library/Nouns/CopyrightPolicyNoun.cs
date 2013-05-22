using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library.NounObject;

namespace OpenAsset.RestClient.Library
{
    public class CopyrightPolicyNoun : RestAPI<CopyrightPolicyObject>
    {

        public CopyrightPolicyNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += COPYRIGHT_POLICIES_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name)
        {
            CopyrightPolicyObject newNounObj = new CopyrightPolicyObject(name);
            CopyrightPolicyObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
