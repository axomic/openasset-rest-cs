using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library.NounObject;

namespace OpenAsset.RestClient.Library
{
    public class PhotographerNoun : RestAPI<PhotographerObject>
    {

        public PhotographerNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += PHOTOGRAPHERS_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name)
        {
            PhotographerObject newNounObj = new PhotographerObject(name);
            PhotographerObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
