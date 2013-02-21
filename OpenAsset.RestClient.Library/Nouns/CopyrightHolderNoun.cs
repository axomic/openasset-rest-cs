using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class CopyrightHolderNoun : RestAPI<CopyrightHolderObject>
    {

        public CopyrightHolderNoun(string baseURL, string username, string password)
            : base(baseURL,username,password)
        {
            _nounURL += COPYRIGHT_HOLDERS_ENDPOINT;
        }


        public PostResponse createNewObjectNoun(string name)
        {
            CopyrightHolderObject newNounObj = new CopyrightHolderObject(name);
            CopyrightHolderObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
