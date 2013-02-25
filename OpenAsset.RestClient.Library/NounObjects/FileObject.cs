using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class FileObject : OARestNounObject
    {
        public long Id { get; protected set; }
        public int AccessLevel { get; set; }
        public string Caption { get; set; }
        public long CategoryId { get; protected set; }
        public long ClickCount { get; protected set; }
        public long CopyrightHolderId { get; set; }
        public string Created { get; protected set; }//needs to be treated has a date
        public int DownloadCount { get; protected set; }
        public double Duration { get; protected set; }
        public string Filename { get; protected set; }
        public string Md5AtUpload { get; protected set; }
        public string Md5AtNow { get; protected set; }
        public long OaUserId { get; protected set; }
        public long PhotographerId { get; set; }
        public long ProjectId { get; protected set; }
        public int Rank { get; set; }
        public int RotationSinceUpload { get; protected set; }
        public string Uploaded { get; protected set; }//needs to be treated has a date
        public bool ContainsAudio { get; protected set; }
        public bool ContainsVideo { get; protected set; }
        public FieldValueObject[] Fields { get; set; }
        public KeywordValueObject[] Keywords { get; set; }
        public SizeValueObject[] Sizes { get; protected set; }

        private bool _postOnlyMandatory = false;

        internal FileObject() { }

        internal FileObject(string filepath, int accessLevel, long categoryId, long projectId = -1)
        {
            _filename = Filename = filepath;
            _accessLevel = AccessLevel = accessLevel;
            _categoryId = CategoryId = categoryId;
            _projectId = ProjectId = projectId;
            _postOnlyMandatory = true;
        }

        internal FileObject(string name, long categoryId, long projectId, long albumId,
            int accessLevel, bool alive, string caption, long copyrightHolderId, string description, long photographerId)
        {
            _name = name;
            _categoryId = CategoryId = categoryId;
            _projectId = ProjectId = projectId;
            _albumId = albumId;
            _accessLevel = AccessLevel = accessLevel;
            _alive = alive;
            _caption = Caption = caption;
            _copyrightHolderId = CopyrightHolderId = copyrightHolderId;
            _description = description;
            _photographerId = PhotographerId = photographerId;
        }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            AccessLevel = _accessLevel;
            Caption = _caption;
            CategoryId = _categoryId;
            ClickCount = _clickCount;
            CopyrightHolderId = _copyrightHolderId;
            Created = _created;//needs to be treated has a date
            DownloadCount = _downloadCount;
            Duration = _duration;
            Filename = _filename;
            Md5AtUpload = _md5AtUpload;
            Md5AtNow = _md5AtNow;
            OaUserId = _oaUserId;
            PhotographerId = _photographerId;
            ProjectId = _projectId;
            Rank = _rank;
            RotationSinceUpload = _rotationSinceUpload;
            Uploaded = _uploaded;//needs to be treated has a date
            ContainsAudio = _containsAudio;
            ContainsVideo = _containsVideo;
            Fields = _fields;
            Keywords = _keywords;
            Sizes = _sizes;
        }

        protected override string getSpecificJson(HttpMethod method)
        {
            string result = null;
            if (method.Equals(HttpMethod.POST))
            {
                result = "{" +
                    "\"category_id\":\"" + _categoryId +
                    (_postOnlyMandatory ? "" : "\",\"name\":\"" + _name) +
                    (_projectId == -1? "" : "\",\"project_id\":\"" + _projectId) +
                    (_postOnlyMandatory?"":"\",\"album_id\":\"" + _albumId) +
                    "\",\"access_level\":\"" + _accessLevel +
                    (_postOnlyMandatory?"":"\",\"alive\":\"" + (_alive?"1":"0")) +
                    (_postOnlyMandatory?"":"\",\"caption\":\"" + _caption) +
                    (_postOnlyMandatory?"":"\",\"copyright_holder_id\":\"" + _copyrightHolderId) +
                    (_postOnlyMandatory?"":"\",\"description\":\"" + _description) +
                    (_postOnlyMandatory?"":"\",\"photographer_id\":\"" + _photographerId) +
                    "\"}";
            }
            return result;
        }
    }
}
