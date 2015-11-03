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
    public class CopyrightHolder : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? copyright_policy_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual int CopyrightPolicyId
        {
            get { return copyright_policy_id ?? default(int); }
            set { copyright_policy_id = value; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
            set { updated = dateTime2DbString(value); }
        }
        #endregion

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            CopyrightHolder otherCopyrightHolder = obj as CopyrightHolder;
            if (otherCopyrightHolder != null)
                return this.name.CompareTo(otherCopyrightHolder.name);
            else
                throw new ArgumentException("Object is not a CopyrightHolder");
        }
    }
}
