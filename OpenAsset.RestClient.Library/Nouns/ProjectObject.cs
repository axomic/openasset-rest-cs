using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.Noun
{
    public class ProjectObject : OARestNounObject
    {
        public long Id { get; set; }
        public bool Alive { get; set; }
        public FieldValueObject[] Fields { get; set; }
        public ProjectKeywordValueObject[] ProjectKeywords { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Alive = _alive;
            ProjectKeywords = _projectKeywords;
            Fields = _fields;
        }
    }
}
