﻿using System;
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string username;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string full_name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? alive;
        // User Sharing extra values
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty, VersionImplemented("8.1.11")]
        protected int? can_modify;
        #endregion

        #region Accessors
        public virtual string UserName
        {
            get { return username; }
            set { username = value; }
        }

        public virtual string FullName
        {
            get { return full_name; }
            set { full_name = value; }
        }

        public virtual bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }

        public virtual bool CanModify
        {
            get { return can_modify != 0; }
            set { can_modify = value ? 1 : 0; }
        }
        #endregion
    }
}
