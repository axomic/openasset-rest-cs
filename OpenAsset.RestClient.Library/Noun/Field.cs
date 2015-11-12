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
    public class Field : Base.BaseNoun, Base.IUpdatedNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? alive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? cardinality;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, VersionImplemented("10.1.0")]
        protected string rest_code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string field_display_type;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string field_type;
        [JsonProperty("protected",NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? _protected;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? built_in;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? include_on_info;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? include_on_search;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;

        // Normal sub-field
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected List<string> values;
        // Grid sub-field
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? limit;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? offset;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? total;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<GridRow> rows;
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

        public virtual string RestCode
        {
            get { return rest_code; }
            set { rest_code = value; }
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

        public virtual bool BuiltIn
        {
            get { return (built_in ?? default(int)) != 0 ? true : false; }
            set { built_in = value ? 1 : 0; }
        }

        public virtual bool IncludeOnInfo
        {
            get { return (include_on_info ?? default(int)) != 0 ? true : false; }
            set { include_on_info = value ? 1 : 0; }
        }

        public virtual bool IncludeOnSearch
        {
            get { return (include_on_search ?? default(int)) != 0 ? true : false; }
            set { include_on_search = value ? 1 : 0; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
        }

        // Normal sub-field
        public virtual List<string> Values
        {
            get
            {
                if (values == null)
                    values = new List<string>();
                return values;
            }
            set
            {
                if (values == null)
                    values = value;
                else
                {
                    values.Clear();
                    values.AddRange(value);
                }
            }
        }

        // Grid sub-field
        public virtual int Limit
        {
            get { return limit ?? default(int); }
            set { limit = value; }
        }

        public virtual int Offset
        {
            get { return offset ?? default(int); }
            set { offset = value; }
        }

        public virtual int Total
        {
            get { return total ?? default(int); }
            set { total = value; }
        }

        public virtual List<GridRow> Rows
        {
            get
            {
                if (rows == null)
                    rows = new List<GridRow>();
                return rows;
            }
            set
            {
                if (rows == null)
                    rows = value;
                else
                {
                    rows.Clear();
                    rows.AddRange(value);
                }
            }
        }
        #endregion

        public virtual bool IsGrid
        {
            get { return rows != null; }
        }

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
