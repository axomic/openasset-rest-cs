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
    public class Keyword : Base.BaseNoun
    {
        int id;
        int keyword_category_id;
        string name;

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("keyword_id")]
        protected int? keyword_id;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (keyword_id.HasValue)
            {
                id = keyword_id.Value;
            }
        }
    }
}
