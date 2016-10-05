using Caterpillar.Core.Collections;
using Caterpillar.Core.Common;
using Caterpillar.Core.Exception;
using System;
using System.Web;
using System.Web.Routing;

namespace Caterpillar.Web.Common
{
    public class CaterpillarHttpRequestBase : HttpRequestBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets base http request object that serves base clas type for the ASP.NET application to read HTTP values during http reuest.
        /// </summary>
        public HttpRequestBase Request { get; set; }

        /// <summary>
        /// Gets or sets http request context that has defined route.
        /// </summary>
        public RequestContext RequestContext { get; set; }

        #endregion

        #region Constructors
                
        public CaterpillarHttpRequestBase(HttpRequestBase httpRequest)
        {
            if (httpRequest == null)
            {
                ArgumentNullOrEmptyException.Throw("httpRequest");
            }

            this.Request = httpRequest;
            this.RequestContext = this.Request.RequestContext;
        }

        /// <summary>
        /// Initialize new CaterpillarHttpRequestBase instance.
        /// </summary>
        /// <param name="requestContext">RequestContext object which has information about an HTTP request that matches a defined route.</param>
        /// <exception cref="">Throws argument null exception, if <paramref name="requestContext"/> is null.</exception>
        public CaterpillarHttpRequestBase(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                ArgumentNullOrEmptyException.Throw("requestContext");
            }

            this.Request = this.RequestContext.HttpContext.Request;
            this.RequestContext = requestContext;
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Gets the type of response from http requst which client waits.
        /// </summary>
        /// <returns>Creates a DataType object from request and returns it.</returns>
        public virtual DataType GetResponseType()
        {
            string responseType = this.Request.QueryString[Constants.QueryString.ResponseType];
            DataType dType = DataType.NotSet;
            if (!string.IsNullOrWhiteSpace(responseType))
            {
                dType = Converter.ToDataType(responseType);
            }

            return dType;
        }
        
        /// <summary>
        /// Gets an http request is ajax or not from Querystring.
        /// </summary>
        /// <returns>Returns true if a request an ajax request.</returns>
        public virtual bool IsAjaxRequest()
        {
            bool isAjax = string.Equals(this.Request.QueryString[Constants.QueryString.IsAjax], Constants.Values.Ajax, StringComparison.OrdinalIgnoreCase);
            return isAjax;
        }

        /// <summary>
        /// Gets or sets the client ip address.
        /// </summary>
        /// <returns>Returns ip address of client in string format.</returns>
        public string GetClientIpAddress()
        {
            string ipAddress = null;
            if (!string.IsNullOrWhiteSpace(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
            }
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }

        /// <summary>
        /// Creates Dictionary&lt;string,string&gt; instance from an HttpBrowserCapabilitiesBase instance.
        /// </summary>
        /// <param name="httpBrowserCapabilitiesBase">Abject that stores browser capabilities.</param>
        /// <returns>Returns an instance of StringToStringDictionary object.</returns>
        public StringToStringDictionary ConvertBrowserInformationToDictionary(HttpBrowserCapabilitiesBase httpBrowserCapabilitiesBase)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
