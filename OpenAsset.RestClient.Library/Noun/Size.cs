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
    public class Size : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? original;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string postfix;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? alive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? always_create;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string colourspace;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? crop_to_fit;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string file_format;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? height;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? quality;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? size_protected;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? use_for_contact_sheet;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? use_for_power_point;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? use_for_zip;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? width;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? x_resolution;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? y_resolution;
        #endregion

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("size_id",NullValueHandling = NullValueHandling.Ignore)]
        protected int? size_id;

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Original
        {
            get { return original != 0 ? true : false; }
            set { original = value ? 1 : 0; }
        }

        public string Postfix
        {
            get { return postfix; }
            set { postfix = value; }
        }

        public bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }

        public bool AlwaysCreate
        {
            get { return (always_create ?? default(int)) != 0 ? true : false; }
            set { always_create = value ? 1 : 0; }
        }

        public string Colourspace
        {
            get { return colourspace; }
            set { colourspace = value; }
        }

        public bool CropToFit
        {
            get { return (crop_to_fit ?? default(int)) != 0 ? true : false; }
            set { crop_to_fit = value ? 1 : 0; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }

        public string FileFormat
        {
            get { return file_format; }
            set { file_format = value; }
        }

        public int Height
        {
            get { return height ?? default(int); }
            set { height = value; }
        }

        public int Quality
        {
            get { return quality ?? default(int); }
            set { quality = value; }
        }

        public bool SizeProtected
        {
            get { return size_protected != 0 ? true : false; }
            set { size_protected = value ? 1 : 0; }
        }

        public bool UseForContactSheet
        {
            get { return use_for_contact_sheet != 0 ? true : false; }
            set { use_for_contact_sheet = value ? 1 : 0; }
        }

        public bool UseForPowerpoint
        {
            get { return use_for_power_point != 0 ? true : false; }
            set { use_for_power_point = value ? 1 : 0; }
        }

        public bool UseForZip
        {
            get { return use_for_zip != 0 ? true : false; }
            set { use_for_zip = value ? 1 : 0; }
        }

        public int Width
        {
            get { return width ?? default(int); }
            set { width = value; }
        }

        public int XResolution
        {
            get { return x_resolution ?? default(int); }
            set { x_resolution = value; }
        }

        public int YResolution
        {
            get { return y_resolution ?? default(int); }
            set { y_resolution = value; }
        }
        #endregion

        protected override void OnDeserialized(StreamingContext context)
        {
            if (size_id.HasValue)
            {
                id = size_id.Value;
            }
        }

        public override string UniqueCode
        {
            get { return postfix; }
            set { postfix = value; }
        }

        public override string UniqueCodeField
        {
            get { return "postfix"; }
        }
    }
}
