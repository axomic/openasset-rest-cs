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
    public class Search : Base.BaseNoun
    {
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int all_users_can_modify;
        [JsonProperty]
        public int approved_company_saved_search;
        [JsonProperty]
        public int can_modify;
        [JsonProperty]
        public string code;
        [JsonProperty]
        public int company_saved_search;
        [JsonProperty]
        public string created;
        [JsonProperty]
        public int locked;
        [JsonProperty]
        public int saved;
        [JsonProperty]
        public int share_with_all_users;
        [JsonProperty]
        public string updated;
        [JsonProperty]
        public int user_id;
        [JsonProperty]
        public List<SearchItem> search_items;

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
            if (search_items == null)
                return searchItems == null;
            if (searchItems == null)
                return false;
            if (search_items.Count != searchItems.Count)
                return false;
            for (int i = 0; i < searchItems.Count; i++)
            {
                if (!searchItems[i].Equals(search_items[i]))
                    return false;
            }
            return true;
        }
    }
}
