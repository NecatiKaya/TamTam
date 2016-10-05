using System.IO;
using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.Extensions
{
    /// <summary>
    /// Contains extension methods for System.Web.Mvc.Controller type.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Returns html of Mvc views.
        /// </summary>
        /// <param name="model">Model for view.</param>
        /// <param name="viewFullPath">View path to render view</param>
        /// <returns>Returns html in string.</returns>
        public static string RenderMvcViewToString(this System.Web.Mvc.ControllerBase controller, object model, string viewFullPath)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewFullPath);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Returns html of ascx object.
        /// </summary>
        /// <typeparam name="T">Type of te model to instaniate view data dictionary</typeparam>
        /// <param name="viewPath">Path of the ascx file to render.</param>
        /// <param name="model">Model to pas .ascx</param>
        /// <param name="masterPagePath">Maser page to render in returned html.</param>
        /// <returns>Returns html in string.</returns>
        public static string RenderFormViewToString<T>(this System.Web.Mvc.ControllerBase controller, string viewPath, T model, string masterPagePath)
        {
            controller.ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var view = new WebFormView(controller.ControllerContext, viewPath, masterPagePath);
                var vdd = new ViewDataDictionary<T>(model);
                var viewCxt = new ViewContext(controller.ControllerContext, view, vdd, new TempDataDictionary(), writer);
                viewCxt.View.Render(viewCxt, writer);
                return writer.ToString();
            }
        }
    }
}
