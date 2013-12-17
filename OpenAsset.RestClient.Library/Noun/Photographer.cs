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
    class Photographer : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty]
        private string name;
        #endregion

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
    }
}