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
    public class Field : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? alive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? cardinality;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string field_display_type;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string field_type;
        [JsonProperty("protected",NullValueHandling = NullValueHandling.Ignore)]
        protected int? _protected;

        //
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<string> values;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual string FieldType
        {
            get { return field_type; }
            set { field_type = value; }
        }

        public virtual bool Protected
        {
            get { return (_protected ?? default(int)) != 0 ? true : false; }
            set { _protected = value ? 1 : 0; }
        }

        public virtual string FieldDisplayType
        {
            get { return field_display_type; }
            set { field_display_type = value; }
        }

        public virtual int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }

        public virtual int Cardinality
        {
            get { return cardinality ?? default(int); }
            set { cardinality = value; }
        }

        public virtual bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }

        public virtual List<string> Values
        {
            get { return values; }
            set { values = value; }
        }
        #endregion


        public override string UniqueCode
        {
            get { return code; }
            set { code = value; }
        }

        public override string UniqueCodeField
        {
            get { return "code"; }
        }

        public override string SearchCode
        {
            get
            {
                return base.SearchCode + "." + id.ToString();
            }
        }
    }
}
