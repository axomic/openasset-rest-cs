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
    public class GridRow
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? _row;
        [JsonExtensionData]
        protected Dictionary<string, object> _additionalData;
        #endregion

        #region Accessors
        public virtual int Row
        {
            get { return _row ?? default(int); }
            set { _row = value; }
        }

        public Dictionary<string, object> AdditionalData
        {
            get { return _additionalData; }
        }
        #endregion
    }
}
