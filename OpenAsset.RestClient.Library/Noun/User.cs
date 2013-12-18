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
    public class User : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string username;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string full_name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? alive;
        #endregion

        #region Accessors
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        public string FullName
        {
            get { return full_name; }
            set { full_name = value; }
        }

        public bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }
        #endregion
    }
}
