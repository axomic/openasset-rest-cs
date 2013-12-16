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
        [JsonProperty]
        private string name;
        [JsonProperty]
        private int alive;
        [JsonProperty]
        private int cardinality;
        [JsonProperty]
        private string code;
        [JsonProperty]
        private string description;
        [JsonProperty]
        private int display_order;
        [JsonProperty]
        private string field_display_type;
        [JsonProperty]
        private string field_type;
        [JsonProperty("protected")]
        public int _protected;

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("field_id")]
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
            get { return _protected != 0 ? true : false; }
            set { _protected = value ? 1 : 0; }
        }

        public string FieldDisplayType
        {
            get { return field_display_type; }
            set { field_display_type = value; }
        }

        public int DisplayOrder
        {
            get { return display_order; }
            set { display_order = value; }
        }

        public int Cardinality
        {
            get { return cardinality; }
            set { cardinality = value; }
        }

        public bool Alive
        {
            get { return alive != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }
        #endregion

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (field_id.HasValue)
            {
                id = field_id.Value;
            }
        }
    }
}
