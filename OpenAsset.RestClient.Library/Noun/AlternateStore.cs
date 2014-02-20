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
    public class AlternateStore : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string storage_name;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string StorageName
        {
            get { return storage_name; }
            set { storage_name = value; }
        }
        #endregion
    }
}
