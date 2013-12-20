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
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string name_alias_1;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string name_alias_2;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string code_alias_1;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string code_alias_2;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? alive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<Field> fields;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<ProjectKeyword> projectKeywords;
        #endregion

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string NameAlias1
        {
            get { return name_alias_1; }
            set { name_alias_1 = value; }
        }

        public string NameAlias2
        {
            get { return name_alias_2; }
            set { name_alias_2 = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string CodeAlias1
        {
            get { return code_alias_1; }
            set { code_alias_1 = value; }
        }

        public string CodeAlias2
        {
            get { return code_alias_2; }
            set { code_alias_2 = value; }
        }

        public bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }

        public List<ProjectKeyword> ProjectKeywords
        {
            get { return projectKeywords; }
            set { projectKeywords = value; }
        }

        public List<Field> Fields
        {
            get { return fields; }
            set { fields = value; }
        }
        #endregion

        public override string UniqueCode
        {
            get { return code; }
            set { code = value; }
        }

        public override string UniqueCodeField
        {
            get { return "code"; }
        }

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
