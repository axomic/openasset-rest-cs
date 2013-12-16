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
    public class Album : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public string code;
        [JsonProperty]
        public string description;
        [JsonProperty]
        public int user_id;
        [JsonProperty]
        public int all_users_can_modify;
        [JsonProperty]
        public int can_modify;
        [JsonProperty]
        public int my_album;
        [JsonProperty]
        public int shared_album;
        [JsonProperty]
        public int company_album;
        [JsonProperty]
        public int share_with_all_users;
        [JsonProperty]
        public int locked;
        [JsonProperty]
        public string private_image_count;
        [JsonProperty]
        public string public_image_count;
        [JsonProperty]
        public string unapproved_image_count;
        [JsonProperty]
        public string updated;
        [JsonProperty]
        public string created;

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Album otherAlbum = obj as Album;
            if (otherAlbum != null)
                return this.name.CompareTo(otherAlbum.name);
            else
                throw new ArgumentException("Object is not a Category");
        }
    }
}
