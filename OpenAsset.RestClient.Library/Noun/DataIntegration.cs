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
    public class DataIntegration : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string data_integration_type;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? alive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string last_ping;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string address;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string last_connect;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string version;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual string DataIntegrationType
        {
            get { return data_integration_type; }
            set { data_integration_type = value; }
        }
        public virtual bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }
        public virtual DateTime LastPing
        {
            get {  return dbString2DateTime(last_ping); }
            set { last_ping = dateTime2DbString(value); }
        }
        public virtual int display_order
        {
            get { return display_order; }
            set { display_order = value; }
        }
        public virtual string Address
        {
            get { return address; }
            set { address = value; }
        }
        public virtual DateTime LastConnect
        {
            get {  return dbString2DateTime(last_connect); }
            set { last_connect = dateTime2DbString(value); }
        }
        public virtual string Version
        {
            get { return version; }
            set { version = value; }
        }
        #endregion
    }
}
