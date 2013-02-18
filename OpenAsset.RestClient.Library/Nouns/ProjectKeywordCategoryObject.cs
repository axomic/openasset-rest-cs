using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.Noun
{
    public class ProjectKeywordCategoryObject : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DisplayOrder { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Code = _code;
            DisplayOrder = _displayOrder;
        }
    }
}
