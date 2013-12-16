using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    class NewItem
    {
        public int new_id;
        public string message;

        public override string ToString()
        {
            return "New item created/updated:" + "\n\tmessage: " + message + "\n\tid: " + new_id;
        }
    }
}
