using Caterpillar.Core.Common;
using Caterpillar.Web.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Caterpillar.Web.Mvc.Common
{
    /// <summary>
    /// Extends CaterpillarMvcHttpRequest object to make Asp.Net Mvc application to read http request for mvc application.
    /// </summary>
    public class CaterpillarMvcHttpRequest : CaterpillarHttpRequestBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets Caterpillar Http request base object that wraps HttpRequestBase object
        /// </summary>
        public CaterpillarHttpRequestBase CaterpillarRequest { get; set; }

        #endregion

        #region Constructors

        public CaterpillarMvcHttpRequest(HttpRequestBase httpRequest)
            : base(httpRequest)
        {
            this.CaterpillarRequest = new CaterpillarHttpRequestBase(httpRequest);
        }

        public CaterpillarMvcHttpRequest(RequestContext requestContext)
            : base(requestContext)
        {
            this.CaterpillarRequest = new CaterpillarHttpRequestBase(requestContext);
        }

        #endregion        

        #region CaterpillarHttpRequestBase

        /// <summary>
        /// Gets type of the response that client waits. To detect type of response, routing data is used.
        /// </summary>
        /// <returns>Creates a DataType object from request and returns it.</returns>
        public override DataType GetResponseType()
        {
            DataType dType = base.GetResponseType();
            if (dType == DataType.NotSet)
            {
                string responseType = null;
                if (this.RequestContext.RouteData.Values[Constants.Route.ResponseType] != null)
                {
                    responseType = this.RequestContext.RouteData.Values[Constants.Route.ResponseType].ToString();
                    dType = Converter.ToDataType(responseType);
                }
            }
            return dType;
        }

        /// <summary>
        /// Gets an http request is ajax or not from request.
        /// </summary>
        /// <returns>Returns true if a request an ajax request.</returns>
        public override bool IsAjaxRequest()
        {
            return this.Request.IsAjaxRequest();
        }

        #endregion
    }
}
