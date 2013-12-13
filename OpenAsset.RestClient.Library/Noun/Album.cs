using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library.Noun
{
    public class Album : Base.BaseNoun
    {
        public string name;
        public string code;
        public string description;
        public int user_id;
        public int all_users_can_modify;
        public int can_modify;
        public int my_album;
        public int shared_album;
        public int company_album;
        public int share_with_all_users;
        public int locked;
        public string private_image_count;
        public string public_image_count;
        public string unapproved_image_count;
        public string updated;
        public string created;
    }
}
