using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class FieldObject : OARestNounObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Alive { get; set; }
        public int Cardinality { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string FieldDisplayType { get; set; }
        public string FieldType { get; set; }
        public bool Protected { get; set; }

        internal FieldObject() { }

        internal FieldObject(string name, long fieldTypeId, long fieldDisplayTypeId)
        {
            Name = _name = name;
            _fieldDisplayTypeId = fieldDisplayTypeId;
            _fieldTypeId = fieldTypeId;
        }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Name = _name;
            Alive = _alive;
            Cardinality = _cardinality;
            Code = _code;
            Description = _description;
            DisplayOrder = _displayOrder;
            FieldDisplayType = _fieldDisplayType;
            FieldType = _fieldType;
            Protected = _protected;
        }
    }
}
