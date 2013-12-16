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
    public class FieldLookupString : Base.BaseNoun
    {
        [JsonProperty]
        public string value;
        [JsonProperty]
        public string description;
        [JsonProperty]
        public int display_order;
        [JsonProperty]
        public Field field;

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
