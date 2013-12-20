using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// serialization stuff
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OpenAsset.RestClient.Library
{
    class NewItem : CustomResponses.Base.BaseCustomResponse
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        protected int new_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string message;
        #endregion

        #region Accessors
        public int NewId
        {
            get { return new_id; }
            set { new_id = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        #endregion

        public override string ToString()
        {
            return "New item created/updated:" + "\n\tmessage: " + message + "\n\tid: " + new_id;
        }
    }
}
