using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    class NounNonExistingPropertyException : RESTAPIException
    {
        public NounNonExistingPropertyException(string message)
            : base(message)
        {

        }
    }
}
