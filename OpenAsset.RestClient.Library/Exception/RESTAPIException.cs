using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace OpenAsset.RestClient.Library
{
    public class RESTAPIException : System.Exception
    {
        private Error _error = null;
        private string _url = null;
        private WebResponse _response = null;
        private WebRequest _request = null;

        public Error ErrorObj
        {
            get
            {
                return _error;
            }
        }

        public WebResponse Response
        {
            get
            {
                return _response;
            }
        }

        public WebRequest Request
        {
            get
            {
                return _request;
            }
        }

        public RESTAPIException()
        {
        }

        public RESTAPIException(string message)
            : base(message)
        {
        }

        public RESTAPIException(string url, Exception e)
            : base(url, e)
        {
            _url = url;
            WebException we = e as WebException;
            if (we != null)
            {
                _response = we.Response;
            }
        }

        public RESTAPIException(string url, Exception e, WebRequest r)
            : this(url, null, e, r)
        {
        }

        public RESTAPIException(string url, Error error, Exception e)
            : this(url, e)
        {
            _error = error;
        }

        public RESTAPIException(string url, Error error, Exception e, WebRequest r)
            : this(url, error, e)
        {
            _request = r;
        }
    }
}
