using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
// serialization stuff
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Globalization;

namespace OpenAsset.RestClient.Library.Noun.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseNoun : IComparable
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, NestedNounProperty]
        protected int id;

        #region Error
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? http_status_code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected string error_message;

        private void cleanError()
        {
            http_status_code = 0;
            error_message = null;
        }

        public bool Failed
        {
            get { return error_message != null; }
        }

        public int ErrorHTTPCode
        {
            get { return (http_status_code  ?? default(int)); }
        }

        public string ErrorMessage
        {
            get { return error_message; }
        }
        #endregion
        
        #region Additional Data
        [JsonExtensionData]
        protected Dictionary<string, object> _additionalData;

        public Dictionary<string, object> AdditionalData
        {
            get { return _additionalData; }
        }

        // Go through _additionalData dictionary and replace JObjects created from grids with proper GridField objects
        private void translateGridFields()
        {
            if (_additionalData == null)
                return;

            foreach (string name in _additionalData.Keys)
            {
                JObject jObject = _additionalData[name] as JObject;
                if (jObject == null)
                    continue;
                try
                {
                    JArray rows = jObject["rows"] as JArray;
                    if (rows == null)
                        continue;
                    _additionalData[name] = jObject.ToObject<GridField>();
                }
                catch (KeyNotFoundException) { /* Do nothing, not an object we want to translate */}
            }
        }
        #endregion

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual string UniqueCode
        {
            get { return id.ToString(); }
            set { id = Convert.ToInt32(value); }
        }

        public virtual string UniqueCodeField
        {
            get { return "id"; }
        }

        public static string GetNoun(Type type)
        {
            string noun = type.Name;
            noun = Regex.Replace(noun, "y$", "ie");
            noun = Regex.Replace(noun, "ch$", "che");
            return noun + "s";
        }

        #region Serialization callbacks
        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            OnSerializing(context);
        }

        public virtual void OnSerializing(StreamingContext context) { }

        [OnSerialized]
        internal void OnSerializedMethod(StreamingContext context)
        {
            OnSerialized(context);
        }

        public virtual void OnSerialized(StreamingContext context) { }

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            cleanError();
            OnDeserializing(context);
        }

        public virtual void OnDeserializing(StreamingContext context) { }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            translateGridFields();
            OnDeserialized(context);
        }

        public virtual void OnDeserialized(StreamingContext context) { }
        #endregion

        public virtual int CompareTo(object obj)
        {
            if (obj == null) return 1;

            BaseNoun noun = obj as BaseNoun;
            if (noun != null)
            {
                if (noun.UniqueCodeField.Equals("id"))
                    return this.Id.CompareTo(noun.Id);
                else
                    return this.UniqueCode.CompareTo(noun.UniqueCode);
            }
            else
                throw new ArgumentException("Object is not a Noun");
        }

        public virtual string SearchCode
        {
            get
            {
                string typeName = this.GetType().Name;
                return Char.ToLowerInvariant(typeName[0]) + typeName.Substring(1);
            }
        }

        public override bool Equals(object obj)
        {
            BaseNoun otherObj = obj as BaseNoun;
            if (otherObj == null)
                return false;
            return otherObj.id == this.id && obj.GetType().Equals(this.GetType());
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode ^= id.GetHashCode();
            hashCode ^= this.GetType().GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            if (Failed)
            {
                return ErrorHTTPCode + " : " + ErrorMessage;
            }
            return SearchCode + ":" + UniqueCodeField + ":" + UniqueCode;
        }

        protected DateTime dbString2DateTime(string dateTimeStr)
        {
            DateTime theDateTime = new DateTime();
            if (!String.IsNullOrEmpty(dateTimeStr) && !dateTimeStr.Equals("0"))
            {
                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo(CultureInfo.InvariantCulture.ToString(), true);
                theDateTime = DateTime.ParseExact(dateTimeStr, Constant.DB_DATE_FORMAT, theCultureInfo);
                return theDateTime;
            }
            else
            {
                theDateTime = DateTime.MinValue;
            }
            return theDateTime;
        }

        protected string dateTime2DbString(DateTime dateTime)
        {
            return dateTime.ToString(Constant.DB_DATE_FORMAT);
        }

        public virtual string ForeignIdField
        {
            get
            {
                string typeName = this.GetType().Name;
                typeName = Regex.Replace(typeName, @"\B[A-Z]", m => "_" + m.ToString().ToLowerInvariant());
                return Char.ToLowerInvariant(typeName[0]) + typeName.Substring(1) + "_id";
            }
        }

        // needs to be tested for reflection speed
        // Goes through all properties/fields with DataMember attributes and tries to copy them from passed object
        public virtual void Replace(BaseNoun obj)
        {
            if (!(this.GetType().Equals(obj.GetType()) || this.GetType().IsSubclassOf(obj.GetType())))
                return;

            BindingFlags allFields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            List<PropertyInfo> properties = this.GetType().GetProperties(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, property.GetValue(obj, null), null);
            }
            List<FieldInfo> fields = this.GetType().GetFields(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (FieldInfo field in fields)
            {
                field.SetValue(this, field.GetValue(obj));
            }
        }

        // Used keys from conditions to check values of object to match up
        public virtual bool DynamicMatch(Dictionary<string, string> conditions)
        {
            BindingFlags allFields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            List<PropertyInfo> properties = this.GetType().GetProperties(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            int matches = 0;
            foreach (PropertyInfo property in properties)
            {
                string name = (Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute).PropertyName;
                if (name == null)
                    name = property.Name;
                if (!conditions.ContainsKey(name))
                    continue;
                string value = property.GetValue(this, null).ToString();
                if (conditions[name].Equals(value))
                    matches++;
            }
            List<FieldInfo> fields = this.GetType().GetFields(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (FieldInfo field in fields)
            {
                string name = (Attribute.GetCustomAttribute(field, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute).PropertyName;
                if (name == null)
                    name = field.Name;
                if (!conditions.ContainsKey(name))
                    continue;
                string value = field.GetValue(this).ToString();
                if (conditions[name].Equals(value))
                    matches++;
            }

            return matches == conditions.Count;
        }

        // Override this method to give custom control of RESTOptions based on Noun
        public virtual RESTOptions<T> GetRESTOptions<T>() where T : Noun.Base.BaseNoun
        {
            RESTOptions<T> options = new RESTOptions<T>();
            return options;
        }
    }
}
