using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenAsset.RestClient.Library.Noun.Base
{
    public class BaseNoun
    {
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
    }
}
