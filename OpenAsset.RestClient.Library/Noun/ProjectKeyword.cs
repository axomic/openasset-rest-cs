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
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? project_keyword_category_id;
        #endregion

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int ProjectKeywordCategoryId
        {
            get { return project_keyword_category_id ?? default(int); }
            set { project_keyword_category_id = value; }
        }
        #endregion

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
