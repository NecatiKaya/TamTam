using Caterpillar.Core.Application;
using Caterpillar.Core.Common;
using Caterpillar.Core.Security;
using Caterpillar.Core.Session;
using Caterpillar.Web.Mvc.Actions;
using Caterpillar.Web.Mvc.Common;
using Caterpillar.Web.Mvc.Models.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Caterpillar.Web.Mvc.ControllerBase
{
    public class CaterpillarControllerBase : Controller
    {

        public virtual new CaterpillarPrincipal User 
        { 
            get 
            {
                CaterpillarPrincipal princial = HttpContext.User as CaterpillarPrincipal;
                if (princial == null)
                {
                    BasicSessionObject sessionData = ApplicationFoundation.Current.SessionService.GetBasicSessionObject();
                    if (sessionData != null)
                    {
                        princial = new CaterpillarPrincipal(sessionData.UserName);
                        princial.FirstName = sessionData.FirstName;
                        princial.LastName = sessionData.LastName;
                        princial.Rights.AddRange(sessionData.Rights.Select(r => r.RightName).ToList());
                        princial.UserId = sessionData.UserId;
                        princial.PersonId = sessionData.PersonId;
                    }
                }
                return princial;
            }
        }

        /// <summary>
        /// Gets culture as string into two parts. 
        /// </summary>
        /// <returns></returns>
        public string GetCulureInfo()
        {
            CultureInfo ci = new CultureInfo("tr-TR");
            ApplicationFoundation current = ApplicationFoundation.Current;
            if (current != null)
            {
                if (Request != null)
                {
                    if (Request.RequestContext != null)
                    {
                        if (Request.RequestContext.RouteData !=null && Request.RequestContext.RouteData.Values != null)
                        {
                            string language = Convert.ToString(Request.RequestContext.RouteData.Values["language"]);
                            string culture = Convert.ToString(Request.RequestContext.RouteData.Values["culture"]);
                            if (!string.IsNullOrWhiteSpace(language) && !string.IsNullOrWhiteSpace(culture))
                            {
                                ci = new CultureInfo(string.Format("{0}-{1}", language, culture));
                            }
                        }
                    }
                }
                else if (current.SessionService != null)
                {
                    HttpContext currentContext = System.Web.HttpContext.Current;
                    /// Maybe you are accessing a resource other then aspx or mvc controller (such as image file) then session can be null.
                    if (currentContext != null && currentContext.Session != null)
                    {
                        BasicSessionObject sessionObject = current.SessionService.GetBasicSessionObject();
                        if (sessionObject != null)
                        {
                            ci = new CultureInfo(sessionObject.Culture);
                        }
                    }
                }
            }
            return ci.ToString();
        }

        public string GetRedirectUrlWithCulture(string action, string controller = null, string culturePart = null, bool addFirstSeperator = false) 
        {
            string localizedUrlPart = culturePart ?? GetCulureInfo();
            string controllerUrlPart = controller ?? this.RouteData.Values["controller"].ToString();
            string url = string.Format("{0}{1}/{2}/{3}", addFirstSeperator ? "/" : "", localizedUrlPart, controllerUrlPart, action);
            return url;
        }

        public string GetRedirectUrl(string action, string controller = null, bool addFirstSeperator = false)
        {
            string controllerUrlPart = controller ?? this.RouteData.Values["controller"].ToString();
            string url = string.Format("{0}{1}/{2}", addFirstSeperator ? "/" : "", controllerUrlPart, action);
            return url;
        }

        /// <summary>
        /// Deletes cookie from response.
        /// </summary>
        /// <param name="cookieName">Cookie to delete.</param>
        public void DeleteCookie(string cookieName)
        {
            HttpCookie cookie = this.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-365);
                this.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Sends a cookie to client
        /// </summary>
        /// <param name="cookieName">Name of the cookie</param>
        /// <param name="value">Value of the cookie</param>
        /// <param name="cookieExpirationDate">Expiration date of the cookie.</param>
        /// <param name="availableToJavascript">Is cookie http only or not which means cookie is available to javascript or not.</param>
        /// <param name="cookiePath">Path of the cookie. Defult is /</param>
        /// <param name="domain">Domain of the cookie. If you use subdomains for the cookie you can not read it on main domain</param>
        /// <returns>Returns the cookie sent to client.</returns>
        public HttpCookie SetCookie(string cookieName, string value, DateTime cookieExpirationDate, bool availableToJavascript, string cookiePath = "/", string domain = null) 
        {
            HttpCookie cookie = new HttpCookie(cookieName, value);
            cookie.Expires = cookieExpirationDate;
            cookie.HttpOnly = !availableToJavascript;
            if (!string.IsNullOrWhiteSpace(domain))
            {
                cookie.Domain = domain;
            }
            this.Response.Cookies.Add(cookie);
            return cookie;
        }

        public HttpCookie SetAuthCookie(CookieSecuirtyMode securityMode, string cookieName, string value, DateTime cookieExpirationDate, bool availableToJavascript, string userData = null, string cookiePath = "/", string domain = null) 
        {
            string cookieValue = value;
            switch (securityMode)
            {
                case CookieSecuirtyMode.FormsAuthentication:
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, cookieValue, DateTime.Now, cookieExpirationDate, true, userData);
                    value = FormsAuthentication.Encrypt(ticket);
                    break;
                case CookieSecuirtyMode.Default:
                default:
                    break;
            }

            return SetCookie(cookieName, value, cookieExpirationDate, availableToJavascript, cookiePath: cookiePath, domain: domain);
        }

        public string GetUserNameFromAuthCookie(CookieSecuirtyMode securityMode, string cookieName, ref string userDataInTicket)
        {
            userDataInTicket = null;
            string userName = null;
            if (this.Request.Cookies != null && this.Request.Cookies.Count > 0)
            {
                HttpCookie cookie = this.Request.Cookies[cookieName];
                if (cookie != null)
                {
                    switch (securityMode)
                    {
                        case CookieSecuirtyMode.FormsAuthentication:
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                            if (ticket != null)
                            {
                                userName = ticket.Name;
                                userDataInTicket = ticket.UserData;
                            }
                            break;
                        case CookieSecuirtyMode.Default:                            
                        default:
                            userName = cookie.Value;
                            break;
                    }
                }
            }
            return userName;
        }

        public void SetSession(string userName, Guid userId, Guid personId, string culture, string firstName, string lastName, string applicationName, IEnumerable<UserRight> rights)
        {
            BasicSessionObject sessionObject = new BasicSessionObject();
            sessionObject.ApplicationName = applicationName;
            sessionObject.Culture = culture;
            sessionObject.SessionStartDate = DateTime.Now;
            sessionObject.UserId = userId;
            sessionObject.UserName = userName;
            sessionObject.FirstName = firstName;
            sessionObject.LastName = lastName;
            sessionObject.Rights = rights;
            sessionObject.PersonId = personId;
            ApplicationFoundation.Current.SessionService.Set(Constants.Session.BasicSessionObject, sessionObject);
        }

        public bool IsAuthenticated()
        {

            //string userName = null;
            //string userData = null;
            //userName = this.GetUserNameFromAuthCookie(CookieSecuirtyMode.FormsAuthentication, CaterpillarWebConfiguration.Current.AuthenticationCookieName, ref userData);
            //return !string.IsNullOrWhiteSpace(userName);

            return HttpContext.Request.IsAuthenticated;
        }

        public void AbandonSession()
        {
            ApplicationFoundation.Current.SessionService.Set(Constants.Session.BasicSessionObject, null);
            Session.Abandon();
        }

        public ModalActionResult ModalPartial(ModalViewModel model)
        {
            ModalActionResult modal = new ModalActionResult(model);
            return modal;
        }
    }    
}
