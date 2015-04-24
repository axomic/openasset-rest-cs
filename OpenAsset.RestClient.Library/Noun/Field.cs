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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? alive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? cardinality;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string code;
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

        //
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
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
                    values.AddRange(value);
            }
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
