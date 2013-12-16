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
    public class ProjectKeyword : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int project_keyword_category_id;

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ProjectKeyword otherProjectKeyword = obj as ProjectKeyword;
            if (otherProjectKeyword != null)
                return this.name.CompareTo(otherProjectKeyword.name);
            else
                throw new ArgumentException("Object is not a ProjectKeyword");
        }
    }
}
