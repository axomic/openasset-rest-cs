using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class KeywordObject : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long KeywordCategoryId { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            KeywordCategoryId = _keywordCategoryId;
        }
    }
}
