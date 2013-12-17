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
    class Result : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty]
        private int file_id;
        #endregion

        #region Accessors
        public int FileId
        {
            get { return file_id; }
            set { file_id = value; }
        }
        #endregion
    }
}
