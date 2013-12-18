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
    public class KeywordCategory : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? category_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? display_order;
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

        public int CategoryId
        {
            get { return category_id ?? default(int); }
            set { category_id = value; }
        }

        public int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }
        #endregion

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            KeywordCategory otherKeywordCategory = obj as KeywordCategory;
            if (otherKeywordCategory != null)
                return this.name.CompareTo(otherKeywordCategory.name);
            else
                throw new ArgumentException("Object is not a KeywordCategory");
        }
    }
}
