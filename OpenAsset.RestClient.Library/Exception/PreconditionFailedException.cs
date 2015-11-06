using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    public class PreconditionFailedException : RESTAPIException
    {
        public PreconditionFailedException(string url, Exception e)
            : base(url, e)
        {
        }

        public PreconditionFailedException(string url, Exception e, System.Net.WebRequest r)
            : base(url, e, r)
        {
        }

        public PreconditionFailedException(string url, Error error, Exception e)
            : base(url, error, e)
        {
        }

        public PreconditionFailedException(string url, Error error, Exception e, System.Net.WebRequest r)
            : base(url, error, e, r)
        {
        }
    }
}
