using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    public class RESTAPIException : System.Exception
    {
        private Error _error = null;
        private string _url = null;

        public Error ErrorObj
        {
            get
            {
                return _error;
            }
        }

        public RESTAPIException()
        {

        }

        public RESTAPIException(string url, Exception e)
            : base(url, e)
        {
            _url = url;
        }

        public RESTAPIException(string url, Error error, Exception e)
            : this(url, e)
        {
            _error = error;
        }
    }
}
