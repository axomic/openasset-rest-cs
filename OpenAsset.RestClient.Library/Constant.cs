using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAsset.RestClient.Library
{
    public static class Constant
    {
        // Urls
        public const string REST_ANONYMOUS_USERNAME = "anonymous";
        public static readonly string[] REST_AUTHENTICATE_URL_EXTENSION = { "/Headers", "/AccessLevels?limit=1" };
        //public const string REST_AUTHENTICATE_URL_EXTENSION = "/Headers";
        //public const string REST_AUTHENTICATE_URL_EXTENSION_FALLBACK = "/AccessLevels?limit=1";
        public const string REST_LOGOUT_EXTENSION = "&logout=logout";
        public const string REST_USER_AGENT = "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0; Revit Plugin)";
        public const string REST_BASE_PATH = "/REST/1";

        // Uncategorized yet
        public const string REST_MIN_VERSION = "7.1.22";
        
        // text
        public const string DB_DATE_FORMAT = "yyyyMMddHHmmss";

        // Numbers
        public const int REST_REQUEST_TIMEOUT = 90000;
        public const int REST_AUTHENTICATE_TIMEOUT = 3000;
    }
}
