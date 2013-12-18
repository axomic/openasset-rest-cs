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
        private string text_match;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string text_replace;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? case_sensitive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? preserve_first_letter_case;
        #endregion

        #region Accessors
        public string TextMatch
        {
            get { return text_match; }
            set { text_match = value; }
        }

        public string TextReplace
        {
            get { return text_replace; }
            set { text_replace = value; }
        }

        public bool CaseSensitive
        {
            get { return (case_sensitive ?? default(int)) != 0; }
            set { case_sensitive = value ? 1 : 0; }
        }

        public bool PreserveFirstLetterCase
        {
            get { return (preserve_first_letter_case ?? default(int)) != 0; }
            set { preserve_first_letter_case = value ? 1 : 0; }
        }
        #endregion
    }
}
