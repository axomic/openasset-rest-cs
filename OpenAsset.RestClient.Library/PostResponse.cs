using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// could have used the JavaScriptSerializer but the Newtonsoft seems to be faster
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OARestClientLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PostResponse
    {
        [JsonProperty("new_id", NullValueHandling = NullValueHandling.Ignore)]
        public int NewId { get; protected set; }
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; protected set; }
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int Code { get; protected set; }
    }
}
