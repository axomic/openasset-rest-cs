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
        public string name;
        public int original;
        public string postfix;
        public int alive;
        public int always_create;
        public string colourspace;
        public int crop_to_fit;
        public string description;
        public int display_order;
        public string file_format;
        public int height;
        public int quality;
        public int size_protected;
        public int use_for_contact_sheet;
        public int use_for_power_point;
        public int use_for_zip;
        public int width;
        public int x_resolution;
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
