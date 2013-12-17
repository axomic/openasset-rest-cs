using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace OpenAsset.RestClient.Library
{
    public class RESTOptions
    {
        // URL parameters
        private int _limit;
        private int _offset;
        private string _displayFields;
        private string _orderBy;
        private Dictionary<string, string> filters;

        public RESTOptions()
        {
            _limit = 0;
            _offset = 0;
            _displayFields = "";
            _orderBy = "";
            filters = new Dictionary<string, string>();
        }

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
                _displayFields = Regex.Replace(value, "[^A-Za-z_,]", "_");
            }
        }

        public string OrderBy
        {
            get { return _orderBy; }
            set
            {
                _orderBy = Regex.Replace(value, "[^A-Za-z_,]", "_");
            }
        }

        public void SetSearchParameter(string parameter, string value)
        {
            parameter = Regex.Replace(parameter, "[^A-Za-z_,]", "_");
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
