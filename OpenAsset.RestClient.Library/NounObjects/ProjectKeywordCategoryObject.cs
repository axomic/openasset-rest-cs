using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class ProjectKeywordCategoryObject : OARestNounObject
    {
        public long Id { get; protected set; }
        public string Name { get; set; }
        public string Code { get; protected set; }
        public int DisplayOrder { get; set; }

        internal ProjectKeywordCategoryObject() { }

        internal ProjectKeywordCategoryObject(string name)
        {
            Name = _name = name;
        }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Code = _code;
            DisplayOrder = _displayOrder;
        }
    }
}
