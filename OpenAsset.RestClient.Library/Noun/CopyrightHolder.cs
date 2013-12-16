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
    public class CopyrightHolder : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int copyright_policy_id;

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            CopyrightHolder otherCopyrightHolder = obj as CopyrightHolder;
            if (otherCopyrightHolder != null)
                return this.name.CompareTo(otherCopyrightHolder.name);
            else
                throw new ArgumentException("Object is not a CopyrightHolder");
        }
    }
}
