using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// serialization stuff
using Newtonsoft.Json.Converters;

namespace OpenAsset.RestClient.Library
{
    class CustomResponseConverter : CustomCreationConverter<CustomResponses.Base.BaseCustomResponse>
    {


        public override CustomResponses.Base.BaseCustomResponse Create(Type objectType)
        {
            return new Error();
            //throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType);
        }
    }
}
