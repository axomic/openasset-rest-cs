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
    public class Field : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int alive;
        [JsonProperty]
        public int cardinality;
        [JsonProperty]
        public string code;
        [JsonProperty]
        public string description;
        [JsonProperty]
        public int display_order;
        [JsonProperty]
        public string field_display_type;
        [JsonProperty]
        public string field_type;
        [JsonProperty("protected")]
        public int _protected;

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("field_id")]
        protected int? field_id;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (field_id.HasValue)
            {
                id = field_id.Value;
            }
        }
    }
}
