using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace OpenAsset.RestClient.Library.Information
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AccessLevelsList
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> GET;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> POST;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> PUT;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private List<int> DELETE;
        #endregion

        #region Accessors
        public List<int> Get
        {
            get { return GET; }
        }

        public List<int> Post
        {
            get { return POST; }
        }

        public List<int> Put
        {
            get { return PUT; }
        }

        public List<int> Delete
        {
            get { return DELETE; }
        }
        #endregion

        public bool Can(string action, int accessLevel)
        {
            switch (action.ToLowerInvariant())
            {
                case "get":
                    if (Get == null)
                        return false;
                    return Get.Contains(accessLevel);
                case "post":
                    if (Post == null)
                        return false;
                    return Post.Contains(accessLevel);
                case "put":
                    if (Put == null)
                        return false;
                    return Put.Contains(accessLevel);
                case "delete":
                    if (Delete == null)
                        return false;
                    return Delete.Contains(accessLevel);
            }
            return false;
        }

        public bool CanGet(int accessLevel)
        {
            return Can("get", accessLevel);
        }

        public bool CanPost(int accessLevel)
        {
            return Can("post", accessLevel);
        }

        public bool CanPut(int accessLevel)
        {
            return Can("put", accessLevel);
        }

        public bool CanDelete(int accessLevel)
        {
            return Can("delete", accessLevel);
        }
    }
}
