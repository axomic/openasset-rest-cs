using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
// could have used the JavaScriptSerializer but the Newtonsoft seems to be faster
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OpenAsset.RestClient.Library.NounObject
{

    // generic OARestNounObject
    [JsonObject(MemberSerialization.OptIn)]
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
        protected FieldValueObject[] _fields { get; set; }
        [JsonProperty("keyword_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _keywordId { get; set; }
        [JsonProperty("sizes", NullValueHandling = NullValueHandling.Ignore)]
        protected SizeValueObject[] _sizes { get; set; }
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        protected KeywordValueObject[] _keywords { get; set; }
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
        protected ProjectKeywordValueObject[] _projectKeywords { get; set; }
        [JsonProperty("copyright_policy_id", NullValueHandling = NullValueHandling.Ignore)]
        protected int _copyrightPolicyId { get; set; }
        [JsonProperty("cardinality", NullValueHandling = NullValueHandling.Ignore)]
        protected int _cardinality { get; set; }
        [JsonProperty("field_display_type", NullValueHandling = NullValueHandling.Ignore)]
        protected string _fieldDisplayType { get; set; }
        [JsonProperty("field_display_type_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _fieldDisplayTypeId { get; set; }
        [JsonProperty("field_type", NullValueHandling = NullValueHandling.Ignore)]
        protected string _fieldType { get; set; }
        [JsonProperty("field_type_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _fieldTypeId { get; set; }
        [JsonProperty("keyword_category_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _keywordCategoryId { get; set; }
        [JsonProperty("project_keyword_category_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _projectKeywordCategoryId { get; set; }
        [JsonProperty("quality", NullValueHandling = NullValueHandling.Ignore)]
        protected int _quality { get; set; }
        [JsonProperty("postfix", NullValueHandling = NullValueHandling.Ignore)]
        protected string _postfix { get; set; }
        [JsonProperty("album_id", NullValueHandling = NullValueHandling.Ignore)]
        protected long _albumId { get; set; }

        // variables that need to have their type changed
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

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            string errorMsg = "ERROR: An error ocurred during serialization. ErrorPath:" + errorContext.Path;
            throw new Exception(errorMsg, errorContext.Error);
        }
        
        private bool strNumberToBool(string str)
        {
            bool result = str != null && str.Equals("1") ? true : false;
            return result;
        }

        // this function can be improved: TODO
        public string ToJson(HttpMethod method)
        {
            string specificJson = getSpecificJson(method);
            if (specificJson != null)
                return specificJson;

            PropertyInfo[] propertyInfos = this.GetType().GetProperties();

            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            ToJson(method, writer);

            return sw.ToString();
        }

        protected void ToJson(HttpMethod method, JsonTextWriter writer)
        {

            PropertyInfo[] propertyInfos = this.GetType().GetProperties();

            writer.WriteStartObject();

            foreach (var info in propertyInfos)
            {
                if (info.GetValue(this, null) != null)
                {
                    string underlinedName = Regex.Replace(info.Name, @"(?<a>(?<!^)((?:[A-Z][a-z])|(?:(?<!^[A-Z]+)[A-Z0-9]+(?:(?=[A-Z][a-z])|$))|(?:[0-9]+)))", @"_${a}");
                    writer.WritePropertyName(underlinedName.ToLower());
                    var prop = info.GetValue(this, null);

                    if (info.PropertyType.IsArray)
                    {
                        object[] obj = (object[])prop;
                        writer.WriteStartArray();
                        foreach(object propertyObj in obj)
                        {
                            if (propertyObj != null)
                            {
                                string propertyStr = propertyToString(method, propertyObj, writer);
                                if (propertyStr != null)
                                    writer.WriteValue(propertyStr);
                            }
                        }
                        writer.WriteEndArray();
                    }
                    else
                    {
                        object obj = prop;
                        writer.WriteValue(propertyToString(method, obj, writer));
                    }
                }
            }

            writer.WriteEndObject();
        }

        private string propertyToString(HttpMethod method, object obj, JsonTextWriter writer)
        {
            string result = null;
            Type typeNounObject = typeof(OARestNounObject);
            Type typeObj = obj.GetType();
            if (typeObj.IsSubclassOf(typeNounObject))
            {
                ((OARestNounObject)obj).ToJson(method, writer);
            }
            else if (typeof(bool) == obj.GetType())
            {
                result = ((bool)obj) ? "1" : "0";
            }
            else
            {
                result = obj.ToString();
            }
            return result;
        }

        protected virtual string getSpecificJson(HttpMethod method)
        {
            return null;
        }

        protected abstract void getVariablesFromParent();
    }
}
