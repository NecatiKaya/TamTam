
namespace Caterpillar.Core.Common
{
    /// <summary>
    /// Contains constant values
    /// </summary>
    public struct Constants
    {
        /// <summary>
        /// Contains contant values for query strings.
        /// </summary>
        public struct QueryString
        {
            /// <summary>
            /// Querystring key for ResponseType query string.
            /// </summary>
            public const string ResponseType = "ResponseType";

            /// <summary>
            /// Querystring key for Ajax query string. This key is used to detect a request is an ajax or not.
            /// </summary>
            public const string IsAjax = "IsAjax ";
        }

        /// <summary>
        /// Contains constant values for session.
        /// </summary>
        public struct Session
        {
            /// <summary>
            /// Session key for SessionObject key.
            /// </summary>
            public const string BasicSessionObject = "BasicSessionObject";
        }

        /// <summary>
        /// Contains constant for Mvc routes.
        /// </summary>
        public struct Route
        {
            /// <summary>
            /// Controller constant for routing.
            /// </summary>
            public const string Controller = "controller";

            /// <summary>
            /// Action constant for routing.
            /// </summary>
            public const string Action = "action";

            /// <summary>
            /// Id constant for routing.
            /// </summary>
            public const string Id = "id";

            /// <summary>
            /// Response type constant for routing.
            /// </summary>
            public const string ResponseType = "responsetype";
        }

        public struct Values
        { 
            /// <summary>
            /// Constant string value for "Ajax".
            /// </summary>
            public const string Ajax = "Ajax ";
        }

        public struct DateTime 
        {
            public const string TurkishDate = "dd.MM.yyyy";
        }
    }
}
