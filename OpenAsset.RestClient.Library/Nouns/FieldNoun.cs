using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class FieldNoun : RestAPI<FieldObject>
    {

        public FieldNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += FIELDS_ENDPOINT;
        }
    }
}
