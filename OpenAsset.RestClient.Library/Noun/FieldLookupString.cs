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
        [JsonProperty("value")]
        public string _value;
        [JsonProperty]
        private string description;
        [JsonProperty]
        private int display_order;
        [JsonProperty]
        private Field field;
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
            get { return display_order; }
            set { display_order = value; }
        }

        public Field Field
        {
            get { return field; }
            set { field = value; }
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
