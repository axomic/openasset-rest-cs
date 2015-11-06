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
    public class Album : Base.BaseNoun, Base.IUpdatedNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? user_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? all_users_can_modify;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? can_modify;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? my_album;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? shared_album;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? company_album;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? share_with_all_users;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? locked;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string private_image_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string public_image_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string unapproved_image_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string created;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, VersionImplemented("8.1.11")]
        protected List<File> files;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, VersionImplemented("8.1.11")]
        protected List<User> users;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty, VersionImplemented("8.1.11")]
        protected List<Group> groups;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual int UserId
        {
            get { return user_id ?? default(int); }
            set { user_id = value; }
        }

        public virtual bool AllUsersCanModify
        {
            get { return (all_users_can_modify ?? default(int)) != 0 ? true : false; }
            set { all_users_can_modify = value ? 1 : 0; }
        }

        public virtual bool CanModify
        {
            get { return (can_modify ?? default(int)) != 0 ? true : false; }
            set { can_modify = value ? 1 : 0; }
        }

        public virtual bool MyAlbum
        {
            get { return (my_album ?? default(int)) != 0 ? true : false; }
            set { my_album = value ? 1 : 0; }
        }

        public virtual bool SharedAlbum
        {
            get { return (shared_album ?? default(int)) != 0 ? true : false; }
            set { shared_album = value ? 1 : 0; }
        }

        public virtual bool CompanyAlbum
        {
            get { return (company_album ?? default(int)) != 0 ? true : false; }
            set { company_album = value ? 1 : 0; }
        }

        public virtual bool ShareWithAllUsers
        {
            get { return (share_with_all_users ?? default(int)) != 0 ? true : false; }
            set { share_with_all_users = value ? 1 : 0; }
        }

        public virtual bool Locked
        {
            get { return (locked ?? default(int)) != 0 ? true : false; }
            set { locked = value ? 1 : 0; }
        }

        public virtual string PrivateImageCount
        {
            get { return private_image_count; }
            set { private_image_count = value; }
        }

        public virtual string PublicImageCount
        {
            get { return public_image_count; }
            set { public_image_count = value; }
        }

        public virtual string UnapprovedImageCount
        {
            get { return unapproved_image_count; }
            set { unapproved_image_count = value; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
        }

        public virtual DateTime Created
        {
            get { return dbString2DateTime(created); }
            set { created = dateTime2DbString(value); }
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

        public virtual List<User> Users
        {
            get
            {
                if (users == null)
                    users = new List<User>();
                return users;
            }
            set
            {
                if (users == null)
                    users = value;
                else
                {
                    users.Clear();
                    users.AddRange(value);
                }
            }
        }

        public virtual List<Group> Groups
        {
            get
            {
                if (groups == null)
                    groups = new List<Group>();
                return groups;
            }
            set
            {
                if (groups == null)
                    groups = value;
                else
                {
                    groups.Clear();
                    groups.AddRange(value);
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

            Album otherAlbum = obj as Album;
            if (otherAlbum != null)
                return this.name.CompareTo(otherAlbum.name);
            else
                throw new ArgumentException("Object is not a Category");
        }
    }
}
