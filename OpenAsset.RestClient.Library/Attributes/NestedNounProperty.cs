using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    class NestedNounProperty : Attribute
    {
    }
}
