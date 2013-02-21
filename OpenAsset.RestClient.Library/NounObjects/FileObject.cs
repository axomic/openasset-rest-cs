using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class FileObject : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public string Caption { get; set; }
        public long CategoryId { get; set; }
        public long ClickCount { get; set; }
        public long CopyrightHolderId { get; set; }
        public string Created { get; set; }//needs to be treated has a date
        public int DownloadCount { get; set; }
        public double Duration { get; set; }
        public string Filename { get; set; }
        public string Md5AtUpload { get; set; }
        public string Md5AtNow { get; set; }
        public long OaUserId { get; set; }
        public long PhotographerId { get; set; }
        public long ProjectId { get; set; }
        public int Rank { get; set; }
        public int RotationSinceUpload { get; set; }
        public string Uploaded { get; set; }//needs to be treated has a date
        public bool ContainsAudio { get; set; }
        public bool ContainsVideo { get; set; }
        public FieldValueObject[] Fields { get; set; }
        public KeywordValueObject[] Keywords { get; set; }
        public SizeValueObject[] Sizes { get; set; }

        internal FileObject() { }

        internal FileObject(string name, long categoryId, long projectId, long albumId, 
            int accessLevel, bool alive, string caption, long copyrightHolderId, string description, long photographerId)
        {
            _name = Name = name;
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
            Name = _name;
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

        protected override string getSpecificJson(string method)
        {
            string result = null;
            if (method.Equals("POST"))
            {
                result = "{\"name\":\"" + _name +
                    "\",\"category_id\":\"" + _categoryId +
                    "\",\"project_id\":\"" + _projectId +
                    "\",\"album_id\":\"" + _albumId +
                    "\",\"access_level\":\"" + _accessLevel +
                    "\",\"alive\":\"" + _alive +
                    "\",\"caption\":\"" + _caption +
                    "\",\"copyright_holder_id\":\"" + _copyrightHolderId +
                    "\",\"description\":\"" + _description +
                    "\",\"photographer_id\":\"" + _photographerId +
                    "\"}";
            }
            return result;
        }
    }
}
