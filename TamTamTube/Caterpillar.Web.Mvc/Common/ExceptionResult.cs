using Caterpillar.Core.Application;
using Caterpillar.Core.Client;
using Caterpillar.Core.Common;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Caterpillar.Web.Mvc.Common
{
    /// <summary>
    /// Extends the type System.Web.Mvc.ActionResult to indicate an exception is occured in an Mvc action.
    /// </summary>
    public class ExceptionResult : ActionResult
    {
        #region Properties

        /// <summary>
        /// Gets or sets the exception which this ExceptionResult is for.
        /// </summary>
        public Exception OccuredException { get; set; }

        /// <summary>
        /// Gets or sets the client information to inform end user.
        /// </summary>
        public JsonResultBase ClientOperation { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an ExceptionResult instance with specified System.Exception information.
        /// </summary>
        /// <param name="ex">Exception information for system.</param>
        public ExceptionResult(Exception ex)
        {
            this.OccuredException = ex;
        }

        /// <summary>
        /// Creates an ExceptionResult instance with specified System.Exception and client information.
        /// </summary>
        /// <param name="ex">Exception information for system.</param>
        /// /// <param name="clientData">client information to inform client.</param>
        public ExceptionResult(Exception ex, JsonResultBase clientOperation)
        {
            this.OccuredException = ex;
            this.ClientOperation = clientOperation;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Executes action for result.
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            CaterpillarMvcHttpRequest mvcRequest = new CaterpillarMvcHttpRequest(context.RequestContext);
            bool isAjax = mvcRequest.IsAjaxRequest();

            HttpResponseBase response = context.HttpContext.Response;

            if (isAjax)
            {
                object errorId = context.Controller.ViewData["ErrorId"] ?? "0";
                object error = context.Controller.ViewData["Error"] ?? string.Empty;

                DataType dataType = mvcRequest.GetResponseType();
                switch (dataType)
                {
                    case DataType.Json:
                        response.Clear();
                        JsonResultBase jrb = new JsonResultBase();
                        jrb.ClientSideAction = ClientSideAction.ShowErrorMessage;
                        jrb.Error(errorId.ToString(), error.ToString());
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string jsonString = serializer.Serialize(jrb);
                        response.Write(jsonString);
                        break;
                    case DataType.Html:
                        if (isAjax)
                        {
                            //if (string.IsNullOrWhiteSpace(partialViewName))
                            //{
                            //    result = context.Controller.RenderMvcViewToString(null, "ErrorPartial.cshtml");
                            //}
                            //else
                            //{
                            //    result = PartialView(partialViewName, modelForUi);
                            //}
                        }
                        break;
                }
            }
            else
            {
                context.HttpContext.Response.Redirect(ApplicationFoundation.Settings.ErrorPageUrl, false);
            }
        }

        #endregion
    }
}
