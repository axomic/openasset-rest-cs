using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace OpenAsset.RestClient.Library.Information
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SecurityList
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> GET;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> POST;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> PUT;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> DELETE;
        #endregion

        #region Accessors
        public List<int> Get
        {
            get { return GET; }
        }

        public List<int> Post
        {
            get { return POST; }
        }

        public List<int> Put
        {
            get { return PUT; }
        }

        public List<int> Delete
        {
            get { return DELETE; }
        }
        #endregion
    }
}
