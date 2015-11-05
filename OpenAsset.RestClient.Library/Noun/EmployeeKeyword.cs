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
    public class EmployeeKeyword : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? employee_keyword_category_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? employee_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual int EmployeeKeywordCategoryId
        {
            get { return employee_keyword_category_id ?? default(int); }
            set { employee_keyword_category_id = value; }
        }

        public virtual int EmployeeCount
        {
            get { return employee_count ?? default(int); }
            set { employee_count = value; }
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
                return base.SearchCode + "." + this.employee_keyword_category_id.ToString();
            }
        }

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            EmployeeKeyword otherEmployeeKeyword = obj as EmployeeKeyword;
            if (otherEmployeeKeyword != null)
                return this.name.CompareTo(otherEmployeeKeyword.name);
            else
                throw new ArgumentException("Object is not an EmployeeKeyword");
        }
    }
}
