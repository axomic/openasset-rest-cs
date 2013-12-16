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
        [JsonProperty]
        private string filename;
        [JsonProperty]
        private string original_filename;
        [JsonProperty]
        private int access_level;
        [JsonProperty]
        private int alternate_store_id;
        [JsonProperty]
        private string caption;
        [JsonProperty]
        private int category_id;
        [JsonProperty]
        private int click_count;
        [JsonProperty]
        private int contains_audio;
        [JsonProperty]
        private int contains_video;
        [JsonProperty]
        private int copyright_holder_id;
        [JsonProperty]
        private string created;
        [JsonProperty]
        private string description;
        [JsonProperty]
        private int download_count;
        [JsonProperty]
        private int duration;
        [JsonProperty]
        private string md5_at_upload;
        [JsonProperty]
        private string md5_now;
        [JsonProperty]
        private int photographer_id;
        [JsonProperty]
        private int project_id;
        [JsonProperty]
        private int rank;
        [JsonProperty]
        private int rotation_since_upload;
        [JsonProperty]
        private string uploaded;
        [JsonProperty]
        private int user_id;
        [JsonProperty]
        private List<Field> fields;
        [JsonProperty]
        private List<Size> sizes;
        [JsonProperty]
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
            get { return access_level; }
            set { access_level = value; }
        }

        public int AlternateStoreId
        {
            get { return alternate_store_id; }
            set { alternate_store_id = value; }
        }

        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        public int CategoryId
        {
            get { return category_id; }
            set { category_id = value; }
        }

        public int ClickCount
        {
            get { return click_count; }
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
            get { return copyright_holder_id; }
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
            get { return download_count; }
            set { download_count = value; }
        }

        public int Duration
        {
            get { return duration; }
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
            get { return photographer_id; }
            set { photographer_id = value; }
        }

        public int ProjectId
        {
            get { return project_id; }
            set { project_id = value; }
        }

        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public int RotationSinceUpload
        {
            get { return rotation_since_upload; }
            set { rotation_since_upload = value; }
        }

        public DateTime Uploaded
        {
            get { return dbString2DateTime(uploaded); }
            set { uploaded = dateTime2DbString(value); }
        }

        public int UserId
        {
            get { return user_id; }
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
    }
}
