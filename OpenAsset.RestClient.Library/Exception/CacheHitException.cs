using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    public class CacheHitException : RESTAPIException
    {
        public CacheHitException(string url, Exception e)
            : base(url, e)
        {

        }

        public CacheHitException(string url, Exception e, System.Net.WebRequest r)
            : base(url, e, r)
        {
        }

        public CacheHitException(string url, Error error, Exception e)
            : base(url, error, e)
        {
        }

        public CacheHitException(string url, Error error, Exception e, System.Net.WebRequest r)
            : base(url, error, e, r)
        {
        }
    }
}
