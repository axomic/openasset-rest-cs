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
    public class Category : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public string code;
        [JsonProperty]
        public string description;
        [JsonProperty]
        public int default_access_level;
        [JsonProperty]
        public int default_rank;
        [JsonProperty]
        public int maximum_rank;
        [JsonProperty]
        public int display_order;
        [JsonProperty]
        public int projects_category;
        [JsonProperty]
        public int alive;
        //used in post
        [JsonProperty]
        public int is_projects_category;

        public override string UniqueCode
        {
            get { return code; }
            set { code = value; }
        }

        public override string UniqueCodeField
        {
            get { return "code"; }
        }

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Category otherCategory = obj as Category;
            if (otherCategory != null)
                return this.name.CompareTo(otherCategory.name);
            else
                throw new ArgumentException("Object is not a Category");
        }
    }
}
