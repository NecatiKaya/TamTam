using Caterpillar.Core.Common;
using System;

namespace Caterpillar.Core.Client
{
    /// <summary>
    /// Represents a client requests.
    /// </summary>
    public class ClientRequest
    {
        /// <summary>
        /// Gets or sets action to take at client side.
        /// </summary>
        public ClientSideAction ClientAction { get; set; }

        /// <summary>
        /// Gets or sets partial html file name. Partial view file name in Mvc and ascx file name in Asp.Net. Can be only useful in Mvc and ASp.Net project. 
        /// </summary>
        public string PartialViewName { get; set; }

        /// <summary>
        /// Gets or sets data type that client awaits from a request's response.
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// Gets or sets the request object. You can think this object an model parameter in mvc or a request parameter in wcf service.
        /// </summary>
        public object RequestObject { get; set; }

        /// <summary>
        /// Gets or sets an action to run in RunSafely block.
        /// </summary>
        public Action SafeAction { get; set; }

        /// <summary>
        /// Gets or sets an action to run in catch block in RunSafely block.
        /// </summary>
        public Action ExceptionAction { get; set; }

        /// <summary>
        /// Gets or sets an action to run if the request object inherits from IValidator and IsNoValid method returns false.
        /// </summary>
        public Action RequestObjectNotValidAction { get; set; }

        /// <summary>
        /// Gets or sets whether to throw an exception for the RequestObject property when its validation fails. 
        /// </summary>
        public bool ThrowExOnBusinessValidationFails { get; set; }

        /// <summary>
        /// Gets or sets navigate url if the ClientAction has Navigate value.
        /// </summary>
        public string NavigateUrl { get; set; }
    }
}
