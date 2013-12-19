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
        private string filename;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string original_filename;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? access_level;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? alternate_store_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string caption;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? category_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? click_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? contains_audio;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? contains_video;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? copyright_holder_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string created;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? download_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? duration;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string md5_at_upload;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string md5_now;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? photographer_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? project_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? rank;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? rotation_since_upload;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string uploaded;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? user_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<Field> fields;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<Size> sizes;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<Keyword> keywords;
        #endregion

        #region Accessors
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public string OriginalFilename
        {
            get { return original_filename; }
            set { original_filename = value; }
        }

        public int AccessLevel
        {
            get { return access_level ?? default(int); }
            set { access_level = value; }
        }

        public int AlternateStoreId
        {
            get { return alternate_store_id ?? default(int); }
            set { alternate_store_id = value; }
        }

        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        public int CategoryId
        {
            get { return category_id ?? default(int); }
            set { category_id = value; }
        }

        public int ClickCount
        {
            get { return click_count ?? default(int); }
            set { click_count = value; }
        }

        public bool ContainsAudio
        {
            get { return contains_audio != 0 ? true : false; }
            set { contains_audio = value ? 1 : 0; }
        }

        public bool ContainsVideo
        {
            get { return contains_video != 0 ? true : false; }
            set { contains_video = value ? 1 : 0; }
        }

        public int CopyrightHolderId
        {
            get { return copyright_holder_id ?? default(int); }
            set { copyright_holder_id = value; }
        }

        public DateTime Created
        {
            get { return dbString2DateTime(created); }
            set { created = dateTime2DbString(value); }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int DownloadCount
        {
            get { return download_count ?? default(int); }
            set { download_count = value; }
        }

        public int Duration
        {
            get { return duration ?? default(int); }
            set { duration = value; }
        }

        public string Md5AtUpload
        {
            get { return md5_at_upload; }
            set { md5_at_upload = value; }
        }

        public string Md5Now
        {
            get { return md5_now; }
            set { md5_now = value; }
        }

        public int PhotographerId
        {
            get { return photographer_id ?? default(int); }
            set { photographer_id = value; }
        }

        public int ProjectId
        {
            get { return project_id ?? default(int); }
            set { project_id = value; }
        }

        public int Rank
        {
            get { return rank ?? default(int); }
            set { rank = value; }
        }

        public int RotationSinceUpload
        {
            get { return rotation_since_upload ?? default(int); }
            set { rotation_since_upload = value; }
        }

        public DateTime Uploaded
        {
            get { return dbString2DateTime(uploaded); }
            set { uploaded = dateTime2DbString(value); }
        }

        public int UserId
        {
            get { return user_id ?? default(int); }
            set { user_id = value; }
        }

        public List<Field> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public List<Size> Sizes
        {
            get { return sizes; }
            set { sizes = value; }
        }

        public List<Keyword> Keywords
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
