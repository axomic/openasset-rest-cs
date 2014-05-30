using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    [AttributeUsage(AttributeTargets.All)]
    class VersionImplemented : Attribute
    {
        public readonly Version Version;

        public VersionImplemented(string versionString = Constant.REST_MIN_VERSION)
        {
            this.Version = new Version(versionString);
        }
    }
}
