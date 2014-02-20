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
    public class File : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string filename;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string original_filename;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? access_level;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? alternate_store_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string caption;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? category_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? click_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? contains_audio;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? contains_video;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? copyright_holder_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string created;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? download_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected float? duration;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string md5_at_upload;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string md5_now;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? photographer_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? project_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? rank;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? rotation_since_upload;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string uploaded;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? user_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<Field> fields;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<Size> sizes;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<Keyword> keywords;
        #endregion

        #region Accessors
        public virtual string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public virtual string OriginalFilename
        {
            get { return original_filename; }
            set { original_filename = value; }
        }

        public virtual int AccessLevel
        {
            get { return access_level ?? default(int); }
            set { access_level = value; }
        }

        public virtual int AlternateStoreId
        {
            get { return alternate_store_id ?? default(int); }
            set { alternate_store_id = value; }
        }

        public virtual string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        public virtual int CategoryId
        {
            get { return category_id ?? default(int); }
            set { category_id = value; }
        }

        public virtual int ClickCount
        {
            get { return click_count ?? default(int); }
            set { click_count = value; }
        }

        public virtual bool ContainsAudio
        {
            get { return contains_audio != 0 ? true : false; }
            set { contains_audio = value ? 1 : 0; }
        }

        public virtual bool ContainsVideo
        {
            get { return contains_video != 0 ? true : false; }
            set { contains_video = value ? 1 : 0; }
        }

        public virtual int CopyrightHolderId
        {
            get { return copyright_holder_id ?? default(int); }
            set { copyright_holder_id = value; }
        }

        public virtual DateTime Created
        {
            get { return dbString2DateTime(created); }
            set { created = dateTime2DbString(value); }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual int DownloadCount
        {
            get { return download_count ?? default(int); }
            set { download_count = value; }
        }

        public virtual float Duration
        {
            get { return duration ?? default(float); }
            set { duration = value; }
        }

        public virtual string Md5AtUpload
        {
            get { return md5_at_upload; }
            set { md5_at_upload = value; }
        }

        public virtual string Md5Now
        {
            get { return md5_now; }
            set { md5_now = value; }
        }

        public virtual int PhotographerId
        {
            get { return photographer_id ?? default(int); }
            set { photographer_id = value; }
        }

        public virtual int ProjectId
        {
            get { return project_id ?? default(int); }
            set { project_id = value; }
        }

        public virtual int Rank
        {
            get { return rank ?? default(int); }
            set { rank = value; }
        }

        public virtual int RotationSinceUpload
        {
            get { return rotation_since_upload ?? default(int); }
            set { rotation_since_upload = value; }
        }

        public virtual DateTime Uploaded
        {
            get { return dbString2DateTime(uploaded); }
            set { uploaded = dateTime2DbString(value); }
        }

        public virtual int UserId
        {
            get { return user_id ?? default(int); }
            set { user_id = value; }
        }

        public virtual List<Field> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public virtual List<Size> Sizes
        {
            get { return sizes; }
            set { sizes = value; }
        }

        public virtual List<Keyword> Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }
        #endregion

        public override string UniqueCode
        {
            get { return filename; }
            set { filename = value; }
        }

        public override string UniqueCodeField
        {
            get { return "filename"; }
        }
    }
}
