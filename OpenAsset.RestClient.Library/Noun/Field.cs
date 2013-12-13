using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library.Noun
{
    public class Field : Base.BaseNoun
    {
        public string name;
        public int alive;
        public int cardinality;
        public string code;
        public string description;
        public int display_order;
        public string field_display_type;
        public string field_type;
        //public int _protected;
    }
}
