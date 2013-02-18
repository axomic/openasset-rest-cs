using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.Noun
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
    }
}
