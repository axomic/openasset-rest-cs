using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library.NounObject
{
    public class ProjectKeywordObject : OARestNounObject
    {
        public long Id { get; protected set; }
        public string Name { get; set; }
        public long ProjectKeywordCategoryId { get; protected set; }

        internal ProjectKeywordObject() { }

        internal ProjectKeywordObject(string name, long projectKeywordCategoryId)
        {
            Name = _name = name;
            ProjectKeywordCategoryId = _projectKeywordCategoryId = projectKeywordCategoryId;
        }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            ProjectKeywordCategoryId = _projectKeywordCategoryId;
        }
    }
}
