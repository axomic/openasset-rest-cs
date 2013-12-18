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
    public class Album : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? user_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? all_users_can_modify;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? can_modify;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? my_album;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? shared_album;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? company_album;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? share_with_all_users;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private int? locked;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string private_image_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string public_image_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string unapproved_image_count;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string updated;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private string created;
        #endregion

        #region Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int UserId
        {
            get { return user_id ?? default(int); }
            set { user_id = value; }
        }

        public bool AllUsersCanModify
        {
            get { return (all_users_can_modify ?? default(int)) != 0 ? true : false; }
            set { all_users_can_modify = value ? 1 : 0; }
        }

        public bool CanModify
        {
            get { return (can_modify ?? default(int)) != 0 ? true : false; }
            set { can_modify = value ? 1 : 0; }
        }

        public bool MyAlbum
        {
            get { return (my_album ?? default(int)) != 0 ? true : false; }
            set { my_album = value ? 1 : 0; }
        }

        public bool SharedAlbum
        {
            get { return (shared_album ?? default(int)) != 0 ? true : false; }
            set { shared_album = value ? 1 : 0; }
        }

        public bool CompanyAlbum
        {
            get { return (company_album ?? default(int)) != 0 ? true : false; }
            set { company_album = value ? 1 : 0; }
        }

        public bool ShareWithAllUsers
        {
            get { return (share_with_all_users ?? default(int)) != 0 ? true : false; }
            set { share_with_all_users = value ? 1 : 0; }
        }

        public bool Locked
        {
            get { return (locked ?? default(int)) != 0 ? true : false; }
            set { locked = value ? 1 : 0; }
        }

        public string PrivateImageCount
        {
            get { return private_image_count; }
            set { private_image_count = value; }
        }

        public string PublicImageCount
        {
            get { return public_image_count; }
            set { public_image_count = value; }
        }

        public string UnapprovedImageCount
        {
            get { return unapproved_image_count; }
            set { unapproved_image_count = value; }
        }

        public DateTime Updated
        {
            get { return dbString2DateTime(updated); }
            set { updated = dateTime2DbString(value); }
        }

        public DateTime Created
        {
            get { return dbString2DateTime(created); }
            set { created = dateTime2DbString(value); }
        }
        #endregion

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
