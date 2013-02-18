using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.Noun
{
    public class PhotographerObject : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
        }
    }
}
