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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? default_access_level;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? default_rank;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? maximum_rank;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? projects_category;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? alive;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual int DefaultAccessLevel
        {
            get { return default_access_level ?? default(int); }
            set { default_access_level = value; }
        }

        public virtual int DefaultRank
        {
            get { return default_rank ?? default(int); }
            set { default_rank = value; }
        }

        public virtual int MaximumRank
        {
            get { return maximum_rank ?? default(int); }
            set { maximum_rank = value; }
        }

        public virtual int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }

        public virtual int ProjectsCategory
        {
            get { return projects_category ?? default(int); }
            set { projects_category = value; }
        }

        public virtual bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
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
