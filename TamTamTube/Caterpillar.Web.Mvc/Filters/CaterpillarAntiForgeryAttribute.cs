using System;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CaterpillarAntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// AntiForgeryConfig.CookieName değerinin defaultu ile ayndır.
        /// </summary>
        private const string _headerName = "__RequestVerificationToken";

        /// <summary>
        /// Gets or sets redirect url when the validation is failed.
        /// </summary>
        public string RedirectUrl { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            #region | Version 1 |

            //if (filterContext == null)
            //{
            //    throw new CoreLevelException("Exception in CaterpillarAntiForgeryAttribute.OnAuthorization(AuthorizationContext filterContext)", new ArgumentNullException("filterContext"));
            //}

            //var httpContext = filterContext.HttpContext;
            //string cookieValue = httpContext.Request.Cookies[_headerName].Value;
            //string headerValue = httpContext.Request.Headers[_headerName];
            //string formValue = headerValue ?? httpContext.Request.Form[_headerName];

            ////AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request[_headerName]);
            //if (!string.Equals(cookieValue, formValue))
            //{
            //    string requestContentType = filterContext.HttpContext.Request.ContentType.ToLower();
            //    if (requestContentType.Contains("json"))
            //    {
            //        JsonResultBase jrb = new JsonResultBase();
            //        jrb.Data = RedirectUrl;
            //        jrb.ClientSideAction = ClientSideAction.Redirect;
            //        JsonResult jr = new JsonResult();
            //        jr.Data = jrb;
            //        filterContext.Result = jr;
            //    }
            //    else
            //    {
            //        filterContext.Result = new RedirectResult(RedirectUrl);
            //    }

            //    //throw new CriticalLevelException("HttpAntiForgeryException is cought. An attack might occur.", new HttpAntiForgeryException());
            //} 

            #endregion | Version 1 |

            var request = filterContext.HttpContext.Request;

            //  Only validate POSTs
            if (request.HttpMethod == WebRequestMethods.Http.Post)
            {
                //  Ajax POSTs and normal form posts have to be treated differently when it comes
                //  to validating the AntiForgeryToken
                if (request.IsAjaxRequest())
                {
                    var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

                    var cookieValue = antiForgeryCookie != null
                        ? antiForgeryCookie.Value
                        : null;

                    AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                }
                else
                {
                    new ValidateAntiForgeryTokenAttribute()
                        .OnAuthorization(filterContext);
                }
            }
        }
    }
}
