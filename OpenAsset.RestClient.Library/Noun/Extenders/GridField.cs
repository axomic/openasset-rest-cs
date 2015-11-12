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
    public class GridField
    {
        #region private serializable properties
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? limit;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? offset;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected int? total;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<GridRow> rows;
        #endregion

        #region Accessors
        public virtual int Limit
        {
            get { return limit ?? default(int); }
            set { limit = value; }
        }

        public virtual int Offset
        {
            get { return offset ?? default(int); }
            set { offset = value; }
        }

        public virtual int Total
        {
            get { return total ?? default(int); }
            set { total = value; }
        }

        public virtual List<GridRow> Rows
        {
            get
            {
                if (rows == null)
                    rows = new List<GridRow>();
                return rows;
            }
            set
            {
                if (rows == null)
                    rows = value;
                else
                {
                    rows.Clear();
                    rows.AddRange(value);
                }
            }
        }
        #endregion
    }
}
