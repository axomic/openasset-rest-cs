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
    public class Category : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty]
        private string name;
        [JsonProperty]
        private string code;
        [JsonProperty]
        private string description;
        [JsonProperty]
        private int default_access_level;
        [JsonProperty]
        private int default_rank;
        [JsonProperty]
        private int maximum_rank;
        [JsonProperty]
        private int display_order;
        [JsonProperty]
        private int projects_category;
        [JsonProperty]
        private int alive;
        //used in post
        //[JsonProperty]
        //private int is_projects_category;
        #endregion

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int DefaultAccessLevel
        {
            get { return default_access_level; }
            set { default_access_level = value; }
        }

        public int DefaultRank
        {
            get { return default_rank; }
            set { default_rank = value; }
        }

        public int MaximumRank
        {
            get { return maximum_rank; }
            set { maximum_rank = value; }
        }

        public int DisplayOrder
        {
            get { return display_order; }
            set { display_order = value; }
        }

        public int ProjectsCategory
        {
            get { return projects_category; }
            set { projects_category = value; }
        }

        public bool Alive
        {
            get { return alive != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }
        #endregion

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
