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
    public class Size : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int original;
        [JsonProperty]
        public string postfix;
        [JsonProperty]
        public int alive;
        [JsonProperty]
        public int always_create;
        [JsonProperty]
        public string colourspace;
        [JsonProperty]
        public int crop_to_fit;
        [JsonProperty]
        public string description;
        [JsonProperty]
        public int display_order;
        [JsonProperty]
        public string file_format;
        [JsonProperty]
        public int height;
        [JsonProperty]
        public int quality;
        [JsonProperty]
        public int size_protected;
        [JsonProperty]
        public int use_for_contact_sheet;
        [JsonProperty]
        public int use_for_power_point;
        [JsonProperty]
        public int use_for_zip;
        [JsonProperty]
        public int width;
        [JsonProperty]
        public int x_resolution;
        [JsonProperty]
        public int y_resolution;

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("size_id")]
        protected int? size_id;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (size_id.HasValue)
            {
                id = size_id.Value;
            }
        }
    }
}
