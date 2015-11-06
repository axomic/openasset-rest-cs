using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    public class NotValidUserException : RESTAPIException
    {
        public NotValidUserException(string url, Exception e)
            : base(url, e)
        {

        }

        public NotValidUserException(string url, Exception e, System.Net.WebRequest r)
            : base(url, e, r)
        {
        }

        public NotValidUserException(string url, Error error, Exception e)
            : base(url, error, e)
        {
        }

        public NotValidUserException(string url, Error error, Exception e, System.Net.WebRequest r)
            : base(url, error, e, r)
        {
        }
    }
}
