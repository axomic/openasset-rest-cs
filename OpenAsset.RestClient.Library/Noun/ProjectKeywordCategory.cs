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
    public class ProjectKeywordCategory : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty]
        private string name;
        [JsonProperty]
        private string code;
        [JsonProperty]
        private int display_order;
        #endregion

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public int DisplayOrder
        {
            get { return display_order; }
            set { display_order = value; }
        }
        #endregion
    }
}
