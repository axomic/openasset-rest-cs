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
    public class KeywordCategory : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public string code;
        [JsonProperty]
        public int category_id;
        [JsonProperty]
        public int display_order;

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
