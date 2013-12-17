using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Reflection;
// serialization stuff
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenAsset.RestClient.Library
{
    public class RESTOptions<T> where T : Noun.Base.BaseNoun
    {
        // URL parameters
        private int _limit;
        private int _offset;
        private string _displayFields;
        private string _orderBy;
        private Dictionary<string, string> filters;

        #region Contructors
        public RESTOptions()
        {
            _limit = 0;
            _offset = 0;
            _displayFields = "";
            _orderBy = "";
            filters = new Dictionary<string, string>();
        }
        #endregion

        #region Accessors
        public int Limit
        {
            get { return _limit; }
            set
            {
                if (value < 0)
                    _limit = 0;
                else
                    _limit = value;
            }
        }

        public int Offset
        {
            get { return _offset; }
            set
            {
                if (value < 0)
                    _offset = 0;
                else
                    _offset = value;
            }
        }

        public string DisplayFields
        {
            get { return _displayFields; }
            set
            {
                string parameter = Regex.Replace(value, "[^A-Za-z_,]", "_");
                validateParameter(parameter);
                _displayFields = parameter;
            }
        }

        public string OrderBy
        {
            get { return _orderBy; }
            set
            {
                string parameter = Regex.Replace(value, "[^A-Za-z_,]", "_");
                validateParameter(parameter);
                _orderBy = parameter;
            }
        }
        #endregion

        #region Search Parameters
        public void SetSearchParameter(string parameter, string value)
        {
            parameter = Regex.Replace(parameter, "[^A-Za-z_,]", "_");
            validateParameter(parameter);
            filters[parameter] = HttpUtility.UrlEncode(value);
        }

        public void RemoveSearchParameter(string parameter)
        {
            parameter = Regex.Replace(parameter, "[^A-Za-z_,]", "_");
            filters.Remove(parameter);
        }

        public string GetSearchParameter(string parameter)
        {
            parameter = Regex.Replace(parameter, "[^A-Za-z_,]", "_");
            if (filters.ContainsKey(parameter))
                return HttpUtility.UrlDecode(filters[parameter]);
            return "";
        }
        #endregion

        #region Filter Validator
        private void validateParameter(string parameter)
        {
            if (!propertyExists(parameter))
            {
                throw new NounNonExistingPropertyException(Constant.EXCEPTION_PROPERTY_NOT_EXISTS + "[" + parameter + "]");
            }
        }

        private bool propertyExists(string propertyName)
        {
            List<string> result = new List<string>();
            BindingFlags allFields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            List<PropertyInfo> properties = typeof(T).GetProperties(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (PropertyInfo property in properties)
            {
                //in case of the property being a reserved word remove the "_" in front of it
                result.Add(Regex.Replace(property.Name, "^_", ""));
            }
            List<FieldInfo> fields = typeof(T).GetFields(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (FieldInfo field in fields)
            {
                //in case of the property being a reserved word remove the "_" in front of it
                result.Add(Regex.Replace(field.Name, "^_", ""));
            }
            return result.Contains(propertyName);
        }
        #endregion

        public string GetUrlParameters()
        {
            string parameters = "limit=" + Limit;
            parameters += "&offset=" + Offset;
            if (!String.IsNullOrEmpty(DisplayFields))
                parameters += "&displayFields=" + DisplayFields;
            if (!String.IsNullOrEmpty(OrderBy))
                parameters += "&orderBy=" + OrderBy;
            foreach (KeyValuePair<string, string> kvp in filters)
            {
                parameters += "&" + kvp.Key + "=" + kvp.Value;
            }

            return parameters;
        }
    }
}
