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
        [JsonProperty]
        private string name;
        [JsonProperty]
        private int original;
        [JsonProperty]
        private string postfix;
        [JsonProperty]
        private int alive;
        [JsonProperty]
        private int always_create;
        [JsonProperty]
        private string colourspace;
        [JsonProperty]
        private int crop_to_fit;
        [JsonProperty]
        private string description;
        [JsonProperty]
        private int display_order;
        [JsonProperty]
        private string file_format;
        [JsonProperty]
        private int height;
        [JsonProperty]
        private int quality;
        [JsonProperty]
        private int size_protected;
        [JsonProperty]
        private int use_for_contact_sheet;
        [JsonProperty]
        private int use_for_power_point;
        [JsonProperty]
        private int use_for_zip;
        [JsonProperty]
        private int width;
        [JsonProperty]
        private int x_resolution;
        [JsonProperty]
        private int y_resolution;
        #endregion

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("size_id")]
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
            get { return alive != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }

        public bool AlwaysCreate
        {
            get { return always_create != 0 ? true : false; }
            set { always_create = value ? 1 : 0; }
        }

        public string Colourspace
        {
            get { return colourspace; }
            set { colourspace = value; }
        }

        public bool CropToFit
        {
            get { return crop_to_fit != 0 ? true : false; }
            set { crop_to_fit = value ? 1 : 0; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int DisplayOrder
        {
            get { return display_order; }
            set { display_order = value; }
        }

        public string FileFormat
        {
            get { return file_format; }
            set { file_format = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Quality
        {
            get { return quality; }
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
            get { return width; }
            set { width = value; }
        }

        public int XResolution
        {
            get { return x_resolution; }
            set { x_resolution = value; }
        }

        public int YResolution
        {
            get { return y_resolution; }
            set { y_resolution = value; }
        }
        #endregion

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (size_id.HasValue)
            {
                id = size_id.Value;
            }
        }
    }
}
