using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// could have used the JavaScriptSerializer but the Newtonsoft seems to be faster
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OARestClientLib
{
    // generic OARestNounObject
    public abstract class OARestNounObject
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        protected string _code { get; set; }
        [JsonProperty("default_access_level", NullValueHandling = NullValueHandling.Ignore)]
        protected int _defaultAccessLevel { get; set; }
        [JsonProperty("default_rank", NullValueHandling = NullValueHandling.Ignore)]
        protected int _defaultRank { get; set; }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        protected string _description { get; set; }
        [JsonProperty("display_order", NullValueHandling = NullValueHandling.Ignore)]
        protected int _displayOrder { get; set; }
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _id { get; set; }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        protected string _name { get; set; }
        [JsonProperty("access_level", NullValueHandling = NullValueHandling.Ignore)]
        protected int _accessLevel { get; set; }
        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        protected string _caption { get; set; }
        [JsonProperty("category_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _categoryId { get; set; }
        [JsonProperty("click_count", NullValueHandling = NullValueHandling.Ignore)]
        protected long _clickCount { get; set; }
        [JsonProperty("copyright_holder_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _copyrightHolderId { get; set; }
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        protected string _created { get; set; }//this value can be either a date or a boolean
        [JsonProperty("download_count", NullValueHandling = NullValueHandling.Ignore)]
        protected int _downloadCount { get; set; }
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        protected double _duration { get; set; }
        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        protected string _filename { get; set; }
        [JsonProperty("md5_at_upload", NullValueHandling = NullValueHandling.Ignore)]
        protected string _md5AtUpload { get; set; }
        [JsonProperty("md5_now", NullValueHandling = NullValueHandling.Ignore)]
        protected string _md5AtNow { get; set; }
        [JsonProperty("oa_user_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _oaUserId { get; set; }
        [JsonProperty("photographer_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _photographerId { get; set; }
        [JsonProperty("project_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _projectId { get; set; }
        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        protected int _rank { get; set; }
        [JsonProperty("rotation_since_upload", NullValueHandling = NullValueHandling.Ignore)]
        protected int _rotationSinceUpload { get; set; }
        [JsonProperty("uploaded", NullValueHandling = NullValueHandling.Ignore)]
        protected string _uploaded { get; set; }//needs to be treated has a date
        [JsonProperty("field_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _fieldId { get; set; }
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        protected string[] _values { get; set; }
        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        protected FieldValueNoun[] _fields { get; set; }
        [JsonProperty("keyword_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _keywordId { get; set; }
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        protected KeywordValueNoun[] _keywords { get; set; }
        [JsonProperty("sizes", NullValueHandling = NullValueHandling.Ignore)]
        protected SizeValueNoun[] _sizes { get; set; }
        [JsonProperty("size_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _sizeId { get; set; }
        [JsonProperty("colourspace", NullValueHandling = NullValueHandling.Ignore)]
        protected string _colourspace { get; set; }
        [JsonProperty("file_format", NullValueHandling = NullValueHandling.Ignore)]
        protected string _fileFormat { get; set; }
        [JsonProperty("filesize", NullValueHandling = NullValueHandling.Ignore)]
        protected int _filesize { get; set; }
        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        protected int _width { get; set; }
        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        protected int _height { get; set; }
        [JsonProperty("resize_steps", NullValueHandling = NullValueHandling.Ignore)]
        protected int _resizeSteps { get; set; }
        [JsonProperty("x_resolution", NullValueHandling = NullValueHandling.Ignore)]
        protected int _xResolution { get; set; }
        [JsonProperty("y_resolution", NullValueHandling = NullValueHandling.Ignore)]
        protected int _yResolution { get; set; }
        [JsonProperty("project_keyword_id", NullValueHandling = NullValueHandling.Ignore)]
        protected int _projectKeywordId { get; set; }
        [JsonProperty("projectKeywords", NullValueHandling = NullValueHandling.Ignore)]
        protected ProjectKeywordValueNoun[] _projectKeywords { get; set; }
        [JsonProperty("copyright_policy_id", NullValueHandling = NullValueHandling.Ignore)]
        protected int _copyrightPolicyId { get; set; }
        [JsonProperty("cardinality", NullValueHandling = NullValueHandling.Ignore)]
        protected int _cardinality { get; set; }
        [JsonProperty("field_display_type", NullValueHandling = NullValueHandling.Ignore)]
        protected string _fieldDisplayType { get; set; }
        [JsonProperty("field_type", NullValueHandling = NullValueHandling.Ignore)]
        protected string _fieldType { get; set; }
        [JsonProperty("keyword_category_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _keywordCategoryId { get; set; }
        [JsonProperty("project_keyword_category_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _projectKeywordCategoryId { get; set; }
        [JsonProperty("quality", NullValueHandling = NullValueHandling.Ignore)]
        protected int _quality { get; set; }
        [JsonProperty("postfix", NullValueHandling = NullValueHandling.Ignore)]
        protected string _postfix { get; set; }

        // variables that need to have their ype changed
        [JsonProperty("alive", NullValueHandling = NullValueHandling.Ignore)]
        private string _aliveStr { get; set; }
        [JsonIgnore]
        protected bool _alive { get; set; }
        [JsonProperty("projects_category", NullValueHandling = NullValueHandling.Ignore)]
        private string _projectsCategoryStr { get; set; }
        [JsonIgnore]
        protected bool _projectsCategory { get; set; }
        [JsonProperty("contains_audio", NullValueHandling = NullValueHandling.Ignore)]
        protected string _containsAudioStr { get; set; }
        [JsonIgnore]
        protected bool _containsAudio { get; set; }
        [JsonProperty("contains_video", NullValueHandling = NullValueHandling.Ignore)]
        protected string _containsVideoStr { get; set; }
        [JsonIgnore]
        protected bool _containsVideo { get; set; }
        [JsonProperty("watermarked", NullValueHandling = NullValueHandling.Ignore)]
        protected string _watermarkedStr { get; set; }
        [JsonIgnore]
        protected bool _watermarked { get; set; }
        [JsonProperty("recreate", NullValueHandling = NullValueHandling.Ignore)]
        protected string _recreateStr { get; set; }
        [JsonIgnore]
        protected bool _recreate { get; set; }
        [JsonProperty("protected", NullValueHandling = NullValueHandling.Ignore)]
        protected string _protectedStr { get; set; }
        [JsonIgnore]
        protected bool _protected { get; set; }
        [JsonProperty("always_create", NullValueHandling = NullValueHandling.Ignore)]
        protected string _alwaysCreateStr { get; set; }
        [JsonIgnore]
        protected bool _alwaysCreate { get; set; }
        [JsonProperty("original", NullValueHandling = NullValueHandling.Ignore)]
        protected string _originalStr { get; set; }
        [JsonIgnore]
        protected bool _original { get; set; }
        [JsonProperty("crop_to_fit", NullValueHandling = NullValueHandling.Ignore)]
        protected string _cropToFitStr { get; set; }
        [JsonIgnore]
        protected bool _cropToFit { get; set; }
        [JsonProperty("size_protected", NullValueHandling = NullValueHandling.Ignore)]
        protected string _sizeProtectedStr { get; set; }
        [JsonIgnore]
        protected bool _sizeProtected { get; set; }
        [JsonProperty("use_for_contact_sheet", NullValueHandling = NullValueHandling.Ignore)]
        protected string _useForContactSheetStr { get; set; }
        [JsonIgnore]
        protected bool _useForContactSheet { get; set; }
        [JsonProperty("use_for_power_point", NullValueHandling = NullValueHandling.Ignore)]
        protected string _userForPowerPointStr { get; set; }
        [JsonIgnore]
        protected bool _userForPowerPoint { get; set; }
        [JsonProperty("use_for_zip", NullValueHandling = NullValueHandling.Ignore)]
        protected string _useForZipStr { get; set; }
        [JsonIgnore]
        protected bool _useForZip { get; set; }


        // serialization methods
        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            // during serialization
        }

        [OnSerialized]
        internal void OnSerializedMethod(StreamingContext context)
        {
            // on the end of serialization
        }

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            // if anything is needed during deserializing
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            _alwaysCreate = strNumberToBool(_alwaysCreateStr);
            _original = strNumberToBool(_originalStr);
            _cropToFit = strNumberToBool(_cropToFitStr);
            _sizeProtected = strNumberToBool(_sizeProtectedStr);
            _useForContactSheet = strNumberToBool(_useForContactSheetStr);
            _userForPowerPoint = strNumberToBool(_userForPowerPointStr);
            _useForZip = strNumberToBool(_useForZipStr);
            _protected = strNumberToBool(_protectedStr);
            _watermarked = strNumberToBool(_watermarkedStr);
            _recreate = strNumberToBool(_recreateStr);
            _containsAudio = strNumberToBool(_containsAudioStr);
            _containsVideo = strNumberToBool(_containsVideoStr);
            _alive = strNumberToBool(_aliveStr);
            _projectsCategory = strNumberToBool(_projectsCategoryStr);
            getVariablesFromParent();
        }

        private bool strNumberToBool(string str)
        {
            bool result = str != null && str.Equals("1") ? true : false;
            return result;
        }

        protected abstract void getVariablesFromParent();
    }


/***
 *      _   _  ____  _    _ _   _  _____ 
 *     | \ | |/ __ \| |  | | \ | |/ ____|
 *     |  \| | |  | | |  | |  \| | (___  
 *     | . ` | |  | | |  | | . ` |\___ \ 
 *     | |\  | |__| | |__| | |\  |____) |
 *     |_| \_|\____/ \____/|_| \_|_____/ 
 *                                       
 *                                       
 */


    public class CategoryNoun : OARestNounObject
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Alive { get; set; }
        public string Code { get; set; }
        public int DefaultAccessLevel { get; set; }
        public int DefaultRank { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool ProjectsCategory { get; set; }

        protected override void getVariablesFromParent() {
            Id = _id;
            Name = _name;
            Alive = _alive;
            Code = _code;
            DefaultAccessLevel = _defaultAccessLevel;
            DefaultRank = _defaultRank;
            Description = _description;
            DisplayOrder = _displayOrder;
            ProjectsCategory = _projectsCategory;
        }
    }

    public class FileNoun : OARestNounObject
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
        public FieldValueNoun[] Fields { get; set; }
        public KeywordValueNoun[] Keywords { get; set; }
        public SizeValueNoun[] Sizes { get; set; }

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

    public class FieldValueNoun : OARestNounObject
    {

        public long FieldId { get; set; }
        public string[] Values { get; set; }

        protected override void getVariablesFromParent()
        {
            FieldId = _fieldId;
            Values = _values;
        }
    }

    public class KeywordValueNoun : OARestNounObject
    {

        public long KeywordId { get; set; }

        protected override void getVariablesFromParent()
        {
            KeywordId = _keywordId;
        }
    }
    
    public class SizeValueNoun : OARestNounObject
    {
        public long SizeId { get; set; }
        public string Colourspace { get; set; }
        public string FileFormat { get; set; }
        public long Filesize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Recreate { get; set; }
        public int ResizeSteps { get; set; }
        public bool Watermarked { get; set; }
        public int XResolution { get; set; }
        public int YResolution { get; set; }

        protected override void getVariablesFromParent()
        {
            SizeId = _sizeId;
            Colourspace = _colourspace;
            FileFormat = _fileFormat;
            Filesize = _filesize;
            Width = _width;
            Height = _height;
            Recreate = _recreate;
            ResizeSteps = _resizeSteps;
            Watermarked = _watermarked;
            XResolution = _xResolution;
            YResolution = _yResolution;
        }
    }

    public class ProjectKeywordValueNoun : OARestNounObject
    {
        public long ProjectKeywordId { get; set; }

        protected override void getVariablesFromParent()
        {
            ProjectKeywordId = _projectKeywordId;
        }
    }

    public class ProjectNoun : OARestNounObject
    {
        public long Id { get; set; }
        public bool Alive { get; set; }
        public FieldValueNoun[] Fields { get; set; }
        public ProjectKeywordValueNoun[] ProjectKeywords { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Alive = _alive;
            ProjectKeywords = _projectKeywords;
            Fields = _fields;
        }
    }

    public class CopyrightHolderNoun : OARestNounObject
    {
        public long Id { get; set; }
        public long CopyrightPolicyId { get; set; }
        public string Name { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            CopyrightPolicyId = _copyrightPolicyId;
            
        }
    }

    public class CopyrightPolicyNoun : OARestNounObject
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Description = _description;
            Code = _code;
        }
    }

    public class FieldNoun : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Alive { get; set; }
        public int Cardinality { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string FieldDisplayType { get; set; }
        public string FieldType { get; set; }
        public bool Protected { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Alive = _alive;
            Cardinality = _cardinality;
            Code = _code;
            Description = _description;
            DisplayOrder = _displayOrder;
            FieldDisplayType = _fieldDisplayType;
            FieldType = _fieldType;
            Protected = _protected;
        }
    }

    public class KeywordCategoryNoun : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DisplayOrder { get; set; }
        public long CategoryId { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            CategoryId = _categoryId;
            Code = _code;
            DisplayOrder = _displayOrder;
        }
    }

    public class KeywordNoun : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long KeywordCategoryId { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            KeywordCategoryId = _keywordCategoryId;
        }
    }

    public class PhotographerNoun : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
        }
    }

    public class ProjectKeywordCategoryNoun : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DisplayOrder { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Code = _code;
            DisplayOrder = _displayOrder;
        }
    }

    public class ProjectKeywordNoun : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProjectKeywordCategoryId { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            ProjectKeywordCategoryId = _projectKeywordCategoryId;
        }
    }

    public class SizeNoun : OARestNounObject
    {
        public bool Alive { get; set; }
        public bool AlwaysCreate { get; set; }
        public string Colourspace { get; set; }
        public bool CropToFit { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string FileFormat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Original { get; set; }
        public string Postfix { get; set; }
        public bool Protected { get; set; }
        public int Quality { get; set; }
        public bool SizeProtected { get; set; }
        public bool UseForContactSheet { get; set; }
        public bool UseForPowerPoint { get; set; }
        public bool UseForZip { get; set; }
        public int XResolution { get; set; }
        public int YResolution { get; set; }

        protected override void getVariablesFromParent()
        {
            Alive = _alive;
            Colourspace = _colourspace;
            Description = _description;
            DisplayOrder = _displayOrder;
            FileFormat = _fileFormat;
            Width = _width;
            Height = _height;
            Id = _id;
            Name = _name;
            Protected = _protected;
            AlwaysCreate = _alwaysCreate;
            Original = _original;
            Postfix = _postfix;
            CropToFit = _cropToFit;
            Quality = _quality;
            SizeProtected = _sizeProtected;
            UseForContactSheet = _useForContactSheet;
            UseForPowerPoint = _userForPowerPoint;
            UseForZip = _useForZip;
            XResolution = _xResolution;
            YResolution = _yResolution;
        }
    }
}
