using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// serialization stuff
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OpenAsset.RestClient.Library.Noun
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Employee : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string descriptor;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? hero_image_id;
        #endregion

        #region Accessors
        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string Descriptor
        {
            get { return descriptor; }
            set { descriptor = value; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
            set { updated = dateTime2DbString(value); }
        }

        public virtual int HeroImageId
        {
            get { return hero_image_id ?? default(int); }
            set { hero_image_id = value; }
        }
        #endregion

        public override string UniqueCode
        {
            get { return code; }
            set { code = value; }
        }

        public override string UniqueCodeField
        {
            get { return "code"; }
        }

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Employee otherEmployee = obj as Employee;
            if (otherEmployee != null)
                return this.code.CompareTo(otherEmployee.code);
            else
                throw new ArgumentException("Object is not an Emlpoyee");
        }
    }
}
