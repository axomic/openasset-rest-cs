using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace OARestClientLib.NounObject
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CategoryObject : OARestNounObject
    {
        public long Id { get; protected set; }
        public string Name { get; set; }
        public bool Alive { get; set; }
        public string Code { get; set; }
        public int DefaultAccessLevel { get; set; }
        public int DefaultRank { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool ProjectsCategory { get; set; }

        internal CategoryObject(string name, bool isProjectsCategory)
        {
            _name = Name = name;
            _projectsCategory = ProjectsCategory = isProjectsCategory;
        }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Alive = _alive;
            Code = _code;
            DefaultAccessLevel = _defaultAccessLevel;
            DefaultRank = _defaultRank;
            Description = _description;
            DisplayOrder = _displayOrder;
            ProjectsCategory = _projectsCategory;
        }
    }
}
