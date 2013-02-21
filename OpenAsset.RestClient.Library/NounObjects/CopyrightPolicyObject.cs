using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class CopyrightPolicyObject : OARestNounObject
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        internal CopyrightPolicyObject() { }

        internal CopyrightPolicyObject(string name)
        {
            Name = _name = name;
        }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Description = _description;
            Code = _code;
        }
    }
}
