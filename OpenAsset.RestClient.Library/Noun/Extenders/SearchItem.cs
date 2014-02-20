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
    public class SearchItem
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string code;
        [JsonProperty("operator",NullValueHandling = NullValueHandling.Ignore)]
        public string _operator;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? exclude;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<int> ids;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<string> values;
        #endregion

        #region Accessors
        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        public virtual bool Exclude
        {
            get { return (exclude ?? default(int)) != 0; }
            set { exclude = value ? 1 : 0; }
        }

        public virtual List<int> Ids
        {
            get { return ids; }
            set { ids = value; }
        }

        public virtual List<string> Values
        {
            get { return values; }
            set { values = value; }
        }
        #endregion

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            SearchItem otherSearchItem = obj as SearchItem;
            if (otherSearchItem != null)
                return this.code.CompareTo(otherSearchItem.code);
            else
                throw new ArgumentException("Object is not a SearchItem");
        }
    }
}
