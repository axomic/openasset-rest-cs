using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.Noun
{
    public class KeywordValueObject : OARestNounObject
    {

        public long KeywordId { get; set; }

        protected override void getVariablesFromParent()
        {
            KeywordId = _keywordId;
        }
    }
}
