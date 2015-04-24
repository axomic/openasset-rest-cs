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
    public class SearchItem : IComparable
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
            get
            {
                if (ids == null)
                    ids = new List<int>();
                return ids;
            }
            set
            {
                if (ids == null)
                    ids = value;
                else
                {
                    ids.Clear();
                    ids.AddRange(value);
                }
            }
        }

        public virtual List<string> Values
        {
            get
            {
                if (values == null)
                    values = new List<string>();
                return values;
            }
            set
            {
                if (values == null)
                    values = value;
                else
                {
                    values.Clear();
                    values.AddRange(value);
                }
            }
        }
        #endregion

        public int CompareTo(object obj)
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
