using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OARestClientLib.NounObject
{
    public class ProjectObject : OARestNounObject
    {
        public long Id { get; protected set; }
        public bool Alive { get; set; }
        public FieldValueObject[] Fields { get; set; }
        public ProjectKeywordValueObject[] ProjectKeywords { get; set; }

        internal ProjectObject() { }

        internal ProjectObject(string name, string code)
        {
            _name = name;
            _code = code;
        }

        protected override void getVariablesFromParent()
        {
            Id = _id;
            Alive = _alive;
            ProjectKeywords = _projectKeywords;
            Fields = _fields;
        }

        protected override string getSpecificJson(HttpMethod method)
        {
            string result = null;
            if(method.Equals(HttpMethod.POST)) {
                result = "{\"name\":\"" + _name + "\",\"code\":\"" + _code + "\"}";
            }

            return result;
        }
    }
}
