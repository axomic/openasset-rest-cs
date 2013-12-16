using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
// serialization stuff
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OpenAsset.RestClient.Library.Noun.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseNoun : IComparable
    {
        [JsonProperty]
        public int id;

        public virtual string UniqueCode
        {
            get { return id.ToString(); }
            set { id = Convert.ToInt32(value); }
        }

        public virtual string UniqueCodeField
        {
            get { return "id"; }
        }

        public static string GetNoun(Type type)
        {
            string noun = type.Name;
            noun = Regex.Replace(noun, "y$", "ie");
            noun = Regex.Replace(noun, "ch$", "che");
            return noun + "s";
        }

        public virtual int CompareTo(object obj)
        {
            if (obj == null) return 1;

            BaseNoun noun = obj as BaseNoun;
            if (noun != null)
                return this.UniqueCodeField.CompareTo(noun.UniqueCodeField);
            else
                throw new ArgumentException("Object is not a Noun");
        }

        public virtual string SearchCode
        {
            get
            {
                string typeName = this.GetType().Name;
                return Char.ToLowerInvariant(typeName[0]) + typeName.Substring(1);
            }
        }

        public override string ToString()
        {
            return SearchCode + ":" + UniqueCodeField + ":" + UniqueCode;
        }

        // Goes through all properties/fields with DataMember attributes and tries to copy them from passed object
        public virtual void Replace(BaseNoun obj)
        {
            if (!this.GetType().Equals(obj.GetType()))
                return;

            BindingFlags allFields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            List<PropertyInfo> properties = this.GetType().GetProperties(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, property.GetValue(obj, null), null);
            }
            List<FieldInfo> fields = this.GetType().GetFields(allFields)
                .Where(x => Attribute.IsDefined(x, typeof(JsonPropertyAttribute)))
                .ToList();
            foreach (FieldInfo field in fields)
            {
                field.SetValue(this, field.GetValue(obj));
            }
        }
    }
}
