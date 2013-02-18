using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class PhotographerNoun : RestAPI<PhotographerObject>
    {

        public PhotographerNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += PHOTOGRAPHERS_ENDPOINT;
        }
    }
}
