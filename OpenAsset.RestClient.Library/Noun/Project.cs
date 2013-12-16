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
    public class Project : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public string name_alias_1;
        [JsonProperty]
        public string name_alias_2;
        [JsonProperty]
        public string code;
        [JsonProperty]
        public string code_alias_1;
        [JsonProperty]
        public string code_alias_2;
        [JsonProperty]
        public int alive;
        [JsonProperty]
        public List<Field> fields;
        [JsonProperty]
        public List<ProjectKeyword> projectKeywords;

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Project otherProject = obj as Project;
            if (otherProject != null)
                return this.name.CompareTo(otherProject.name);
            else
                throw new ArgumentException("Object is not a Project");
        }
    }
}
