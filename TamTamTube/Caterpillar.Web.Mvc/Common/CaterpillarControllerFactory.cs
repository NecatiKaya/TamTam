using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Caterpillar.Web.Mvc.Common
{
    /// <summary>
    /// Extends the System.Web.Mvc.DefaultControllerFactory controller factory
    /// </summary>
    public class CaterpillarControllerFactory : DefaultControllerFactory
    {
        #region Public Methods

        /// <summary>
        ///  Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The context of the HttpContext contains http request and route.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>The controller.</returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            CaterpillarController caterpillarController = new CaterpillarController();
            caterpillarController.CaterpillarMvcHttpRequest = new CaterpillarMvcHttpRequest(requestContext);
            //return base.CreateController(requestContext, controllerName);
            return caterpillarController;
        }

        /// <summary>
        /// Gets the instance of the control from http request
        /// </summary>
        /// <param name="requestContext">The context of the HttpContext contains http request and route.</param>
        /// <param name="controllerType">Type of the conroller to get instance of.</param>
        /// <returns>Returns the controller.</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return base.GetControllerInstance(requestContext, controllerType);
        }

        /// <summary>
        /// Releases the specified controller to gain resources.
        /// </summary>
        /// <param name="controller">The controller to release.</param>
        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        }

        #endregion
    }
}
