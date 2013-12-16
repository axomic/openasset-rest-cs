using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library.Noun
{
    public class Category : Base.BaseNoun
    {
        public string name;
        public string code;
        public string description;
        public int default_access_level;
        public int default_rank;
        public int maximum_rank;
        public int display_order;
        public int projects_category;
        public int alive;
        //used in post
        public int is_projects_category;
    }
}
