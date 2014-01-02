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
    public class SearchItem : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string code;
        [JsonProperty("operator",NullValueHandling = NullValueHandling.Ignore)]
        public string _operator;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? exclude;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<int> ids;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<string> values;
        #endregion

        #region Accessors
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        public bool Exclude
        {
            get { return (exclude ?? default(int)) != 0; }
            set { exclude = value ? 1 : 0; }
        }

        public List<int> Ids
        {
            get { return ids; }
            set { ids = value; }
        }

        public List<string> Values
        {
            get { return values; }
            set { values = value; }
        }
        #endregion
        
      
        /*
        public override string ToString()
        {
            string text = Char.ToUpperInvariant(code[0]) + code.Substring(1);
            int id = 0;
            if (code.Contains('.'))
            {
                id = Convert.ToInt32(text.Substring(text.IndexOf('.') + 1));
                text = text.Substring(0, text.IndexOf('.'));
                switch (text)
                {
                    case "Field":
                        Field field = RESTEngine.Instance.GetField(id);
                        if (field != null)
                            text = field.name;
                        break;
                    case "Keyword":
                        KeywordCategory keywordCategory = RESTEngine.Instance.GetKeywordCategory(id);
                        if (keywordCategory != null)
                            text = keywordCategory.name;
                        break;
                    case "ProjectKeyword":
                        ProjectKeywordCategory projectKeywordCategory = RESTEngine.Instance.GetProjectKeywordCategory(id);
                        if (projectKeywordCategory != null)
                            text = projectKeywordCategory.name;
                        break;
                }
            }
            text += ": ";
            if (values != null && values.Count > 0)
            {
                text += String.Join(", ", values.ToArray());
            }
            if (ids != null && ids.Count > 0)
            {
                List<string> idStrings = BLL.RESTEngine.Instance.GetSearchStringsFromIds(code, ids);
                text += String.Join(", ", idStrings.ToArray());
            }
            return text;
        }
        */
        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            SearchItem otherSearchItem = obj as SearchItem;
            if (otherSearchItem != null)
                return this.code.CompareTo(otherSearchItem.code);
            else
                throw new ArgumentException("Object is not a SearchItem");
        }
    }
}
