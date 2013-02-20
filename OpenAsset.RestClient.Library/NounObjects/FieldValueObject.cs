using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class FieldValueObject : OARestNounObject
    {

        public long FieldId { get; set; }
        public string[] Values { get; set; }

        protected override void getVariablesFromParent()
        {
            FieldId = _fieldId;
            Values = _values;
        }
    }
}
