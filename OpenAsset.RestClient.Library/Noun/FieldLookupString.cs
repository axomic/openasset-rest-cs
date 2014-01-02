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
    public class FieldLookupString : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty("value",NullValueHandling = NullValueHandling.Ignore)]
        public string _value;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? field_id;
        #endregion

        #region Accessors
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }

        public int FieldId
        {
            get { return field_id ?? default(int); }
            set { field_id = value; }
        }
        #endregion

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            FieldLookupString otherFieldLookupString = obj as FieldLookupString;
            if (otherFieldLookupString != null)
                return this.ToString().CompareTo(otherFieldLookupString.ToString());
            else
                throw new ArgumentException("Object is not a FieldLookupString");
        }
    }
}
