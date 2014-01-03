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
    }
}
