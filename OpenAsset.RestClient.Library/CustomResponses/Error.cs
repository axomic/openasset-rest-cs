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
    public class Error : CustomResponses.Base.BaseCustomResponse
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int http_status_code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string error_message;
        #endregion

        #region Accessors
        public int HttpStatusCode
        {
            get { return http_status_code; }
            set { http_status_code = value; }
        }

        public string ErrorMessage
        {
            get { return error_message; }
            set { error_message = value; }
        }
        #endregion

        public override string ToString()
        {
            return "Error:" + "\n\tmessage: " + error_message + "\n\tcode: " + http_status_code;
        }
    }
}
