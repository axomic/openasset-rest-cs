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
    public class AccessLevel : Base.BaseNoun
    {
        [JsonProperty]
        public string label;

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            AccessLevel otherAccessLevel = obj as AccessLevel;
            if (otherAccessLevel != null)
                return this.id.CompareTo(otherAccessLevel.id);
            else
                throw new ArgumentException("Object is not an AccessLevel");
        }
    }
}
