using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library.Noun
{
    public class File : Base.BaseNoun
    {
        public string filename;
        public string original_filename;
        public int access_level;
        public int alternate_store_id;
        public string caption;
        public int category_id;
        public int click_count;
        public int contains_audio;
        public int contains_video;
        public int copyright_holder_id;
        public string created;
        public string description;
        public int download_count;
        public int duration;
        public string md5_at_upload;
        public string md5_now;
        public int photographer_id;
        public int project_id;
        public int rank;
        public int rotation_since_upload;
        public string uploaded;
        public int user_id;
        //public List<Field> fields;
        //public List<Size> sizes;
        //public List<Keyword> keywords;
    }
}
