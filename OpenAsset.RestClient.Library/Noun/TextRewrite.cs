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
    public class TextRewrite : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string text_match;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string text_replace;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? case_sensitive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? preserve_first_letter_case;
        #endregion

        #region Accessors
        public virtual string TextMatch
        {
            get { return text_match; }
            set { text_match = value; }
        }

        public virtual string TextReplace
        {
            get { return text_replace; }
            set { text_replace = value; }
        }

        public virtual bool CaseSensitive
        {
            get { return (case_sensitive ?? default(int)) != 0; }
            set { case_sensitive = value ? 1 : 0; }
        }

        public virtual bool PreserveFirstLetterCase
        {
            get { return (preserve_first_letter_case ?? default(int)) != 0; }
            set { preserve_first_letter_case = value ? 1 : 0; }
        }
        #endregion
    }
}
