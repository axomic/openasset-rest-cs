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
    public class AspectRatio : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string label;
        #endregion

        #region Accessors
        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string Label
        {
            get { return label; }
            set { label = value; }
        }
        #endregion
    }
}
