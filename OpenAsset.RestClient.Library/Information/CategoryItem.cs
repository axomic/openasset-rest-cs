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
    public class CategoryItem : Base.BaseOptions
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private SecurityList Files;
        #endregion

        public bool Can(string action, int accessLevel)
        {
            if (Files == null)
                return false;
            switch (action.ToLowerInvariant())
            {
                case "get":
                    if (Files.Get == null)
                        return false;
                    return Files.Get.Contains(accessLevel);
                case "post":
                    if (Files.Post == null)
                        return false;
                    return Files.Post.Contains(accessLevel);
                case "put":
                    if (Files.Put == null)
                        return false;
                    return Files.Put.Contains(accessLevel);
                case "delete":
                    if (Files.Delete == null)
                        return false;
                    return Files.Delete.Contains(accessLevel);
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
