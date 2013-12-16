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
    public class File : Base.BaseNoun
    {
        [JsonProperty]
        public string filename;
        [JsonProperty]
        public string original_filename;
        [JsonProperty]
        public int access_level;
        [JsonProperty]
        public int alternate_store_id;
        [JsonProperty]
        public string caption;
        [JsonProperty]
        public int category_id;
        [JsonProperty]
        public int click_count;
        [JsonProperty]
        public int contains_audio;
        [JsonProperty]
        public int contains_video;
        [JsonProperty]
        public int copyright_holder_id;
        [JsonProperty]
        public string created;
        [JsonProperty]
        public string description;
        [JsonProperty]
        public int download_count;
        [JsonProperty]
        public int duration;
        [JsonProperty]
        public string md5_at_upload;
        [JsonProperty]
        public string md5_now;
        [JsonProperty]
        public int photographer_id;
        [JsonProperty]
        public int project_id;
        [JsonProperty]
        public int rank;
        [JsonProperty]
        public int rotation_since_upload;
        [JsonProperty]
        public string uploaded;
        [JsonProperty]
        public int user_id;
        [JsonProperty]
        public List<Field> fields;
        [JsonProperty]
        public List<Size> sizes;
        [JsonProperty]
        public List<Keyword> keywords;
    }
}
