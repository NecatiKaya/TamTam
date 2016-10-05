using Caterpillar.Core.Application;
using Caterpillar.Web.Mvc.Filters;
using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.ControllerBase
{
    [CaterpillarAntiForgeryAttribute]
    public class AuhorizationControllerBase : CaterpillarControllerBase
    {
        //protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        //{
        //    bool authenticated = false;
        //    ActionResult redirectResult =  ReturnToHomePageIfNotAuthenticated(out authenticated);
        //    bool isAjax = Request.IsAjaxRequest();
        //    if (Request.IsAjaxRequest())
        //    {
        //        if (!authenticated)
        //        {
        //            filterContext.Result = new JsonResult()
        //            {
        //                ContentType = "json",
        //                Data = new
        //                {
        //                    ReturnUrl = GetRedirectUrlWithCulture("Index", "Home", addFirstSeperator: true)
        //                }
        //            };
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        if (!authenticated)
        //        {
        //            filterContext.Result = redirectResult;
        //            return;
        //        }
        //    }

        //    base.OnActionExecuting(filterContext);              
        //}

        public ActionResult ReturnToHomePageIfNotAuthenticated(out bool authenticated, string redirectUrl = null) 
        {
            var sessionObject = ApplicationFoundation.Current.SessionService.GetBasicSessionObject();
            if (sessionObject == null)
            {
                string url = GetRedirectUrlWithCulture("Index", "Home", addFirstSeperator: true);
                if (!string.IsNullOrWhiteSpace(redirectUrl))
                {
                    url = redirectUrl;
                }

                authenticated = false;
                return Redirect(url);
            }
            authenticated = true;
            return null;
        }
    }
}
