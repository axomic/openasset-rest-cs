using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    public class Error
    {
        public int http_status_code;
        public string error_message;

        public override string ToString()
        {
            return "Error:" + "\n\tmessage: " + error_message + "\n\tcode: " + http_status_code;
        }
    }
}
