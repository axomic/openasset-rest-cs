using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class ProjectKeywordObject : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProjectKeywordCategoryId { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            ProjectKeywordCategoryId = _projectKeywordCategoryId;
        }
    }
}
