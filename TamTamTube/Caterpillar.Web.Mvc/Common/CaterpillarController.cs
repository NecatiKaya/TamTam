using Caterpillar.Core;
using Caterpillar.Core.Application;
using Caterpillar.Core.Collections;
using Caterpillar.Core.Common;
using Caterpillar.Core.Exception;
using Caterpillar.Core.Extensions;
using Caterpillar.Core.Validation;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.Common
{
    /// <summary>
    /// Extedns System.Web.Mvc.Controller type
    /// </summary>
    public class CaterpillarController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets or sets Caterpillar mvc http request that wraps httprequest object.
        /// </summary>
        public CaterpillarMvcHttpRequest CaterpillarMvcHttpRequest { get; set; }

        /// <summary>
        /// Gets or sets last error information for the last request.
        /// </summary>
        public string Error
        {
            get
            {
                if (base.ViewData["Error"] != null)
                {
                    return base.ViewData["Error"].ToString();
                }

                return null;
            }
            set
            {
                base.ViewData["Error"] = value;
            }
        }

        /// <summary>
        /// Gets or sets last error id for the last request.
        /// </summary>
        public object ErrorId
        {
            get
            {
                return base.ViewData["ErrorId"];
            }
            set
            {
                base.ViewData["ErrorId"] = value;
            }        
        }

        /// <summary>
        /// Gets user associated with http request.
        /// </summary>
        protected virtual new CaterpillarUser User
        {
            get { return this.CaterpillarMvcHttpRequest.RequestContext.HttpContext.User as CaterpillarUser; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles exception and returns action result to take.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        public virtual ExceptionResult Exception(Exception ex)
        {
            ExceptionResult result = null;
            if (ex.IsNotNull())
            {
                ExceptionInformation informationAbouException = new ExceptionInformation(ex);
                if (this.CaterpillarMvcHttpRequest.Browser != null)
                {
                    informationAbouException.ExtraInformation = this.CaterpillarMvcHttpRequest.CaterpillarRequest.ConvertBrowserInformationToDictionary(this.CaterpillarMvcHttpRequest.Browser);
                }
                informationAbouException.ClientIp = this.CaterpillarMvcHttpRequest.GetClientIpAddress();
                if (this.User != null)
                {
                    informationAbouException.UserCultureSettings = this.User.Culture.TwoLetterISOLanguageName;
                    informationAbouException.UserId = this.User.UserId;
                }
                ObjectListCollection objectIds = ApplicationFoundation.Current.ExceptionManager.Log(informationAbouException);
                if (objectIds != null && objectIds.Count > 0)
                {
                    this.ErrorId = objectIds.Where(_id => _id.GetType() == typeof(int) || _id.GetType() == typeof(Guid)).FirstOrDefault();
                }
                this.Error = LanguageStrings.GeneralExceptionMessage;
                result = new ExceptionResult(ex);
            }

            return result;
        }

        /// <summary>
        /// Gets result action. This helper method can simply controller's actions by writing less code.
        /// </summary>
        /// <typeparam name="TMvcModel">Mvc model that is used in view.</typeparam>
        /// <typeparam name="TBusinessModel">Business model of UI model.</typeparam>
        /// <param name="model">Model that is passed from UI to controller by post action</param>
        /// <param name="runSafelyValidAction">A func method that returns ActionResult and is used in RunSafely 'try' block</param>
        /// <param name="businessModel"></param>
        /// <param name="businessModelNotValidAction">Action to take when the business model is an istance of IValidator and is not valid</param>
        /// <param name="throwExOnBusinessValidationFails">If true, throws an exception when business model validation fails after businessModelNotValidAction action method is called. If this parameter is false, no exeption is thrown and businessModelNotValidAction is called.</param>
        /// <returns>Returns an ActionResult</returns>
        public ActionResult RunSafelyInAction<TMvcModel, TBusinessModel>(TMvcModel model, Action runSafelyValidAction, ref TBusinessModel businessModel, Action businessModelNotValidAction = null, bool throwExOnBusinessValidationFails = false)
        {
            ActionResult result = null;
            if (model is ITypeMapper<TBusinessModel>)
            {
                businessModel = ((ITypeMapper<TBusinessModel>)model).ToType();
                if (businessModel is IValidator)
                {
                    IValidator businessModelValidator = (IValidator)businessModel;
                    if (!businessModelValidator.IsValid())
                    {
                        if (businessModelNotValidAction != null)
                        {
                            businessModelNotValidAction();
                            if (throwExOnBusinessValidationFails)
                            {
                                ValidationFailedException.Throw(typeof(TBusinessModel).Name);
                            }
                        }
                    }
                    else
                    {
                        if (businessModelValidator.Errors != null && businessModelValidator.Errors.Keys.Count > 0)
                        {
                            foreach (string eachKey in businessModelValidator.Errors.Keys)
                            {
                                this.ModelState.AddModelError(eachKey, businessModelValidator.Errors[eachKey]);
                            }
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    this.RunSafely(runSafelyValidAction,
                    (ex) =>
                    {
                        result = Exception(ex);
                    });                    
                }                
            }

            //if (result is ExceptionResult)
            //{
            //    return result;
            //}

            //bool isAjaxRequest = this.CaterpillarMvcHttpRequest.IsAjaxRequest();
            //DataType dataType = this.CaterpillarMvcHttpRequest.GetResponseType();
            //switch (dataType)
            //{
            //    case DataType.Json:
            //        JsonResultBase jrb = new JsonResultBase();
            //        jrb.Success(businessModel);
            //        jrb.ClientSideAction = clientActionToactiontoTakeInClient;
            //        result = Json(jrb, JsonRequestBehavior.AllowGet);
            //        break;
            //    case DataType.Html:
            //        if (isAjaxRequest)
            //        {
            //            if (string.IsNullOrWhiteSpace(partialViewName))
            //            {
            //                result = PartialView(businessModel);
            //            }
            //            else
            //            {
            //                result = PartialView(partialViewName, businessModel);
            //            }
            //        }
            //        else
            //        {
            //            result = View(businessModel);
            //        }
            //        break;
            //    case DataType.Text:
            //    case DataType.Xml:
            //    case DataType.NotSet:
            //    default:
            //        break;
            //}
            //return result;

            return result;
        }

        /// <summary>
        /// Gets result action. This helper method can simply controller's actions by writing less code.
        /// </summary>
        /// <param name="runSafelyValidAction">A func method that returns ActionResult and is used in RunSafely 'try' block</param>
        /// <returns>Returns an ActionResult</returns>
        public ActionResult RunSafelyInAction(Action runSafelyValidAction)
        {
            ActionResult result = null;
            if (ModelState.IsValid)
            {
                this.RunSafely(runSafelyValidAction, (ex) =>
                {
                    result = Exception(ex);
                });
            }

            //if (result is ExceptionResult)
            //{
            //    return result;
            //}

            //bool isAjaxRequest = this.CaterpillarMvcHttpRequest.IsAjaxRequest();
            //DataType dataType = this.CaterpillarMvcHttpRequest.GetResponseType();
            //switch (dataType)
            //{
            //    case DataType.Json:
            //        JsonResultBase jsonModel = new JsonResultBase();
            //        jsonModel.Success(modelForUi);
            //        result = Json(jsonModel, JsonRequestBehavior.AllowGet);
            //        break;
            //    case DataType.Html:
            //        if (isAjaxRequest)
            //        {
            //            if (string.IsNullOrWhiteSpace(partialViewName))
            //            {
            //                result = PartialView(modelForUi);
            //            }
            //            else
            //            {
            //                result = PartialView(partialViewName, modelForUi);
            //            }
            //        }
            //        else
            //        {
            //            result = View(modelForUi);
            //        }
            //        break;
            //    case DataType.Text:
            //    case DataType.Xml:
            //    case DataType.NotSet:
            //    default:
            //        break;
            //}
            //return result;

            return result;
        }

        #endregion
    }
}
