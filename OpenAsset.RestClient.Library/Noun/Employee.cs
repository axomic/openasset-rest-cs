using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// serialization stuff
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace OpenAsset.RestClient.Library.Noun
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Employee : Base.BaseNoun, Base.IUpdatedNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string descriptor;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? hero_image_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected List<EmployeeKeyword> employeeKeywords;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected List<Project> projects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected List<File> files;
        #endregion

        #region Accessors
        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string Descriptor
        {
            get { return descriptor; }
            set { descriptor = value; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
        }

        public virtual int HeroImageId
        {
            get { return hero_image_id ?? default(int); }
            set { hero_image_id = value; }
        }

        public virtual List<EmployeeKeyword> EmployeeKeywords
        {
            get
            {
                if (employeeKeywords == null)
                    employeeKeywords = new List<EmployeeKeyword>();
                return employeeKeywords;
            }
            set
            {
                if (employeeKeywords == null)
                    employeeKeywords = value;
                else
                {
                    employeeKeywords.Clear();
                    employeeKeywords.AddRange(value);
                }
            }
        }

        public virtual List<Project> Projects
        {
            get
            {
                if (projects == null)
                    projects = new List<Project>();
                return projects;
            }
            set
            {
                if (projects == null)
                    projects = value;
                else
                {
                    projects.Clear();
                    projects.AddRange(value);
                }
            }
        }

        public virtual List<File> Files
        {
            get
            {
                if (files == null)
                    files = new List<File>();
                return files;
            }
            set
            {
                if (files == null)
                    files = value;
                else
                {
                    files.Clear();
                    files.AddRange(value);
                }
            }
        }
        #endregion

        public override string UniqueCode
        {
            get { return code; }
            set { code = value; }
        }

        public override string UniqueCodeField
        {
            get { return "code"; }
        }

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Employee otherEmployee = obj as Employee;
            if (otherEmployee != null)
                return this.code.CompareTo(otherEmployee.code);
            else
                throw new ArgumentException("Object is not an Emlpoyee");
        }
    }
}
