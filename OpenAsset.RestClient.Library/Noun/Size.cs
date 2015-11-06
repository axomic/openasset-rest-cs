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
    public class Size : Base.BaseNoun, Base.IUpdatedNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? original;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string postfix;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? alive;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? always_create;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected string colourspace;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? crop_to_fit;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? display_order;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected string file_format;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected int? height;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected int? quality;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? size_protected;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? use_for_contact_sheet;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? use_for_power_point;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? use_for_zip;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected int? width;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected int? x_resolution;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected int? y_resolution;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        // size value extra fields
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected int? filesize;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected string http_root;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected string relative_path;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty, VersionImplemented("9.0.0")]
        protected string http_relative_path;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected string unc_root;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty, VersionImplemented("9.0.0")]
        protected int? cropped;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected int? recreate;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), NestedNounProperty]
        protected int? watermarked;
        #endregion

        #region Accessors
        public virtual int Filesize
        {
            get { return filesize ?? default(int); }
            set { filesize = value; }
        }

        public virtual bool Cropped
        {
            get { return cropped != 0; }
            set { cropped = value ? 1 : 0; }
        }

        public virtual bool Recreate
        {
            get { return recreate != 0; }
            set { recreate = value ? 1 : 0; }
        }

        public virtual bool Watermarked
        {
            get { return watermarked != 0; }
            set { watermarked = value ? 1 : 0; }
        }

        public virtual string HttpRoot
        {
            get { return http_root; }
            set { http_root = value; }
        }

        public virtual string RelativePath
        {
            get { return relative_path; }
            set { relative_path = value; }
        }

        public virtual string HttpRelativePath
        {
            get { return http_relative_path; }
            set { http_relative_path = value; }
        }

        public virtual string UncRoot
        {
            get { return unc_root; }
            set { unc_root = value; }
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual bool Original
        {
            get { return original != 0 ? true : false; }
            set { original = value ? 1 : 0; }
        }

        public virtual string Postfix
        {
            get { return postfix; }
            set { postfix = value; }
        }

        public virtual bool Alive
        {
            get { return (alive ?? default(int)) != 0 ? true : false; }
            set { alive = value ? 1 : 0; }
        }

        public virtual bool AlwaysCreate
        {
            get { return (always_create ?? default(int)) != 0 ? true : false; }
            set { always_create = value ? 1 : 0; }
        }

        public virtual string Colourspace
        {
            get { return colourspace; }
            set { colourspace = value; }
        }

        public virtual bool CropToFit
        {
            get { return (crop_to_fit ?? default(int)) != 0 ? true : false; }
            set { crop_to_fit = value ? 1 : 0; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual int DisplayOrder
        {
            get { return display_order ?? default(int); }
            set { display_order = value; }
        }

        public virtual string FileFormat
        {
            get { return file_format; }
            set { file_format = value; }
        }

        public virtual int Height
        {
            get { return height ?? default(int); }
            set { height = value; }
        }

        public virtual int Quality
        {
            get { return quality ?? default(int); }
            set { quality = value; }
        }

        public virtual bool SizeProtected
        {
            get { return size_protected != 0 ? true : false; }
            set { size_protected = value ? 1 : 0; }
        }

        public virtual bool UseForContactSheet
        {
            get { return use_for_contact_sheet != 0 ? true : false; }
            set { use_for_contact_sheet = value ? 1 : 0; }
        }

        public virtual bool UseForPowerpoint
        {
            get { return use_for_power_point != 0 ? true : false; }
            set { use_for_power_point = value ? 1 : 0; }
        }

        public virtual bool UseForZip
        {
            get { return use_for_zip != 0 ? true : false; }
            set { use_for_zip = value ? 1 : 0; }
        }

        public virtual int Width
        {
            get { return width ?? default(int); }
            set { width = value; }
        }

        public virtual int XResolution
        {
            get { return x_resolution ?? default(int); }
            set { x_resolution = value; }
        }

        public virtual int YResolution
        {
            get { return y_resolution ?? default(int); }
            set { y_resolution = value; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
        }
        #endregion

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
