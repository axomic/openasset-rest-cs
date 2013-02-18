using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class CopyrightHolderObject : OARestNounObject
    {
        public long Id { get; set; }
        public long CopyrightPolicyId { get; set; }
        public string Name { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            CopyrightPolicyId = _copyrightPolicyId;

        }
    }
}
