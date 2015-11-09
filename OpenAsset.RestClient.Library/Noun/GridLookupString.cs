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
    public class GridLookupString : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty("value",NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        public string _value;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected int? display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected int? grid_column_id;
        #endregion

        #region Accessors
        public virtual string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public virtual int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }

        public virtual int GridColumnId
        {
            get { return grid_column_id ?? default(int); }
            set { grid_column_id = value; }
        }
        #endregion

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            GridLookupString otherGridLookupString = obj as GridLookupString;
            if (otherGridLookupString != null)
                return this.ToString().CompareTo(otherGridLookupString.ToString());
            else
                throw new ArgumentException("Object is not a GridLookupString");
        }
    }
}
