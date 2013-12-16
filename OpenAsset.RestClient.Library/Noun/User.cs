using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// serialization stuff
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OpenAsset.RestClient.Library.Noun
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User : Base.BaseNoun
    {
        [JsonProperty]
        public string username;
        [JsonProperty]
        public string full_name;
        [JsonProperty]
        public int alive;
    }
}
