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

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("field_id",NullValueHandling = NullValueHandling.Ignore)]
        protected int? field_id;
        #endregion

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string FieldType
        {
            get { return field_type; }
            set { field_type = value; }
        }

        public bool Protected
        {
            get { return (_protected ?? default(int)) != 0 ? true : false; }
            set { _protected = value ? 1 : 0; }
        }

        public string FieldDisplayType
        {
            get { return field_display_type; }
            set { field_display_type = value; }
        }

        public int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }

        public int Cardinality
        {
            get { return cardinality ?? default(int); }
            set { cardinality = value; }
        }

        public bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }

        public List<string> Values
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

        protected override void OnDeserialized(StreamingContext context)
        {
            if (field_id.HasValue)
            {
                id = field_id.Value;
            }
        }
    }
}
