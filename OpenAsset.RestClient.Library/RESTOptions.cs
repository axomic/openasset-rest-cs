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
        private List<string> _displayFields;
        private List<string> _orderBy;
        private Dictionary<string, string> _filters;

        #region Contructors
        public RESTOptions()
        {
            _limit = 0;
            _offset = 0;
            _displayFields = new List<string>();
            _orderBy = new List<string>();
            _filters = new Dictionary<string, string>();
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
        #endregion

        #region OrderBy
        public void AddOrderBy(string field, bool ascending = true)
        {
            string postfix = ascending ? "Asc" : "Desc";
            field = Regex.Replace(field, "[^A-Za-z_,]", "_");
            validateParameter(field);
            //used Insert just to make it clear to add to the end of the list
            _orderBy.Insert(_orderBy.Count,field + postfix);
        }

        public bool RemoveOrderByd(string field)
        {
            field = Regex.Replace(field, "[^A-Za-z_,]", "_");
            bool result = _orderBy.Remove(field + "Asc");
            result = result || _orderBy.Remove(field + "Desc");
            return result;
        }

        public List<string> GetOrderBy()
        {
            return _orderBy;
        }

        public void ClearOrderBy()
        {
            _orderBy.Clear();
        }
        #endregion

        #region DisplayFields
        public void AddDisplayField(string field)
        {
            field = Regex.Replace(field, "[^A-Za-z_,]", "_");
            validateParameter(field);
            _displayFields.Add(field);
        }

        public bool RemoveDisplayField(string field)
        {
            field = Regex.Replace(field, "[^A-Za-z_,]", "_");
            return _displayFields.Remove(field);
        }

        public List<string> GetDisplayFields()
        {
            return _displayFields;
        }

        public void ClearDisplayFields()
        {
            _displayFields.Clear();
        }
        #endregion

        #region Search Parameters
        public void SetSearchParameter(string parameter, string value)
        {
            parameter = Regex.Replace(parameter, "[^A-Za-z_,]", "_");
            //validateParameter(parameter);
            _filters[parameter] = HttpUtility.UrlEncode(value);
        }

        public void RemoveSearchParameter(string parameter)
        {
            parameter = Regex.Replace(parameter, "[^A-Za-z_,]", "_");
            _filters.Remove(parameter);
        }

        public string GetSearchParameter(string parameter)
        {
            parameter = Regex.Replace(parameter, "[^A-Za-z_,]", "_");
            if (_filters.ContainsKey(parameter))
                return HttpUtility.UrlDecode(_filters[parameter]);
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

        // needs to be tested for reflection speed
        private bool propertyExists(string propertyName)
        {
            List<string> result = new List<string>();
            BindingFlags allFields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            List<PropertyInfo> properties = typeof(T).GetProperties(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (PropertyInfo property in properties)
            {
                string name = (Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute).PropertyName;
                if (name == null)
                    name = property.Name;
                //in case of the property being a reserved word remove the "_" in front of it
                result.Add(name);
            }
            List<FieldInfo> fields = typeof(T).GetFields(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (FieldInfo field in fields)
            {
                string name = (Attribute.GetCustomAttribute(field, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute).PropertyName;
                if (name == null)
                    name = field.Name;
                //in case of the property being a reserved word remove the "_" in front of it
                result.Add(name);
            }
            return result.Contains(propertyName);
        }
        #endregion

        public string GetUrlParameters()
        {
            string parameters = "limit=" + Limit;
            parameters += "&offset=" + Offset;
            string displayFields = String.Join(",", _displayFields.ToArray());
            if (!String.IsNullOrEmpty(displayFields))
                parameters += "&displayFields=" + displayFields;
            string orderBy = String.Join(",", _orderBy.ToArray());
            if (!String.IsNullOrEmpty(orderBy))
                parameters += "&orderBy=" + orderBy;
            foreach (KeyValuePair<string, string> kvp in _filters)
            {
                parameters += "&" + kvp.Key + "=" + kvp.Value;
            }

            return parameters;
        }
    }
}
