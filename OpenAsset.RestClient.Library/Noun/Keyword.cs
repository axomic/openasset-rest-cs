﻿using System;
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
    public class Keyword : Base.BaseNoun
    {
        #region private serializable properties
        [JsonProperty]
        private int keyword_category_id;
        [JsonProperty]
        private string name;
        #endregion

        #region Accessors
        public int KeywordCategoryId
        {
            get { return keyword_category_id; }
            set { keyword_category_id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        // sets the id of the object (when deserialization is made from an expanded field)
        [JsonProperty("keyword_id")]
        protected int? keyword_id;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (keyword_id.HasValue)
            {
                id = keyword_id.Value;
            }
        }

        public override int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Keyword otherKeyword = obj as Keyword;
            if (otherKeyword != null)
                return this.name.CompareTo(otherKeyword.name);
            else
                throw new ArgumentException("Object is not a Keyword");
        }
    }
}