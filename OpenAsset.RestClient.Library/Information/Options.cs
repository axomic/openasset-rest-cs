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
    public class Options
    {
        #region private serializable properties
        [JsonProperty("Files", NullValueHandling = NullValueHandling.Ignore)]
        private AccessLevelsList files;
        // TODO: NYI parameters, future release
        // Object GET;
        // Object PUT;
        // Object POST;
        // Object DELETE;
        #endregion

        #region Accessors
        public AccessLevelsList Files
        {
            get
            {
                return files;
            }
        }
        #endregion
    }
}
