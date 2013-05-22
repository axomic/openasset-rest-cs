using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenAsset.RestClient.Library.NounObject;

namespace OpenAsset.RestClient.Library
{
    public class FieldNoun : RestAPI<FieldObject>
    {

        public FieldNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += FIELDS_ENDPOINT;
        }

        public PostResponse createNewObjectNoun(string name, long fieldTypeId, long fieldDisplayTypeId)
        {
            FieldObject newNounObj = new FieldObject(name, fieldTypeId,fieldDisplayTypeId);
            FieldObject[] newNounObjArray = { newNounObj };
            PostResponse[] response = this.postNounObjects(newNounObjArray);
            return response[0];
        }
    }
}
