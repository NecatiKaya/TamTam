using Caterpillar.Core.Application;
using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.Extensions
{
    public static class WebViewPageHelper
    {
        public static MvcHtmlString GetFromCache(this WebViewPage razorPage, string cacheKey) 
        {
            ApplicationFoundation current = ApplicationFoundation.Current;
            string value = current.ResourceLocalization.GetResourceValue(cacheKey);
            return new MvcHtmlString(value);
        }
    }
}
