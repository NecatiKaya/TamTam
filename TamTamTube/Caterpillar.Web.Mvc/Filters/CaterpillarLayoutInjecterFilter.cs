using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.Filters
{
    /// <summary>
    /// Provids option to an mvc controller action to selects it master/layout.
    /// </summary>
    public class CaterpillarLayoutInjecterFilter : ActionFilterAttribute
    {
        private readonly string _masterName;
        public CaterpillarLayoutInjecterFilter(string masterName)
        {
            _masterName = masterName;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = _masterName;
            }
        }
    }
}
