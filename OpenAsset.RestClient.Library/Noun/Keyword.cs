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
    public class Keyword : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? keyword_category_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        #endregion

        #region Accessors
        public virtual int KeywordCategoryId
        {
            get { return keyword_category_id ?? default(int); }
            set { keyword_category_id = value; }
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
            set { updated = dateTime2DbString(value); }
        }
        #endregion

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Keyword otherKeyword = obj as Keyword;
            if (otherKeyword != null)
                return this.name.CompareTo(otherKeyword.name);
            else
                throw new ArgumentException("Object is not a Keyword");
        }

        public override string SearchCode
        {
            get
            {
                return base.SearchCode + "." + this.keyword_category_id.ToString();
            }
        }
    }
}
