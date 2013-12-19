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

        //public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        //{
        //    while (reader.Read())
        //    {
        //        /*if (reader.Value != null)
        //            Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
        //        else
        //            Console.WriteLine("Token: {0}", reader.TokenType);*/

        //        if (reader.TokenType.Equals("PropertyName"))
        //        {
        //            if (reader.Value.Equals("error_message"))
        //            {
        //                //found property error_message so this is an Error object
        //                return base.ReadJson(reader, typeof(Error), existingValue, serializer);
        //            }                
        //        }
        //    }
        //    //didn't found the property error_message so this is a NewItem object
        //    return base.ReadJson(reader, typeof(NewItem), existingValue, serializer);
        //}
    }
}
