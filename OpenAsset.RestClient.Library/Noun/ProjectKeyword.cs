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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? project_keyword_category_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual int ProjectKeywordCategoryId
        {
            get { return project_keyword_category_id ?? default(int); }
            set { project_keyword_category_id = value; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
        }
        #endregion

        public override string SearchCode
        {
            get
            {
                return base.SearchCode + "." + this.project_keyword_category_id.ToString();
            }
        }

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
