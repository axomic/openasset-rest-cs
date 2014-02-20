using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace OpenAsset.RestClient.Library.Information.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseOptions
    {
        // TODO: NYI parameters, future release
        // Object GET;
        // Object PUT;
        // Object POST;
        // Object DELETE;
    }
}
