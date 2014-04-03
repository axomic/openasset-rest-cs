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
        public const string REST_LOGOUT_EXTENSION = "?logout=logout";
        public const string REST_BASE_PATH = "/REST/1";

        // Uncategorized yet, currently covers all features in this library
        public const string REST_MIN_VERSION = "8.1.7";
        
        // text
        public const string DB_DATE_FORMAT = "yyyyMMddHHmmss";

        // Numbers
        public const int DEFAULT_REST_REQUEST_TIMEOUT = 90000;
        public const int DEFAULT_REST_AUTHENTICATE_TIMEOUT = 3000;

        // headers
        public const string HEADER_FULL_RESULTS_COUNT = "X-Full-Results-Count";
        public const string HEADER_DISPLAY_RESULTS_COUNT = "X-Display-Results-Count";
        public const string HEADER_OFFSET = "X-Offset";
        public const string HEADER_OPENASSET_VERSION = "X-OpenAsset-Version";
        public const string HEADER_SESSIONKEY = "X-SessionKey";
        public const string HEADER_TIMING = "X-Timing";
        public const string HEADER_USER_ID = "X-User-Id";
        public const string HEADER_USERNAME = "X-Username";

        // exception messages
        public const string EXCEPTION_PROPERTY_NOT_EXISTS = "This property does not exist: ";
    }
}
