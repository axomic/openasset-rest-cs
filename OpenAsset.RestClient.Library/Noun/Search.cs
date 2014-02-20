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
    public class Search : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? all_users_can_modify;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? approved_company_saved_search;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? can_modify;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? company_saved_search;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string created;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? locked;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? saved;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? share_with_all_users;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected string updated;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected int? user_id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore), BaseNounProperty]
        protected List<SearchItem> search_items;
        #endregion

        #region Accessors
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual int AllUsersCanModify
        {
            get { return all_users_can_modify ?? default(int); }
            set { all_users_can_modify = value; }
        }

        public virtual bool ApprovedCompanySavedSearch
        {
            get { return (approved_company_saved_search ?? default(int)) != 0 ? true : false; }
            set { approved_company_saved_search = value ? 1 : 0; }
        }

        public virtual bool CanModify
        {
            get { return (can_modify ?? default(int)) != 0 ? true : false; }
            set { can_modify = value ? 1 : 0; }
        }

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual bool CompanySavedSearch
        {
            get { return (company_saved_search ?? default(int)) != 0 ? true : false; }
            set { company_saved_search = value ? 1 : 0; }
        }

        public virtual DateTime Created
        {
            get { return dbString2DateTime(created); }
            set { created = dateTime2DbString(value); }
        }

        public virtual bool Locked
        {
            get { return (locked ?? default(int)) != 0 ? true : false; }
            set { locked = value ? 1 : 0; }
        }

        public virtual bool Saved
        {
            get { return (saved ?? default(int)) != 0 ? true : false; }
            set { saved = value ? 1 : 0; }
        }

        public virtual bool ShareWithAllUsers
        {
            get { return (share_with_all_users ?? default(int)) != 0 ? true : false; }
            set { share_with_all_users = value ? 1 : 0; }
        }

        public virtual DateTime Updated
        {
            get { return dbString2DateTime(updated); }
            set { updated = dateTime2DbString(value); }
        }

        public virtual int UserId
        {
            get { return user_id ?? default(int); }
            set { user_id = value; }
        }

        public virtual List<SearchItem> SearchItems
        {
            get { return search_items; }
            set { search_items = value; }
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

        public bool MatchingSearchItems(List<SearchItem> searchItems)
        {
            if (SearchItems == null)
                return searchItems == null;
            if (searchItems == null)
                return false;
            if (SearchItems.Count != searchItems.Count)
                return false;
            for (int i = 0; i < searchItems.Count; i++)
            {
                if (!searchItems[i].Equals(SearchItems[i]))
                    return false;
            }
            return true;
        }
    }
}
