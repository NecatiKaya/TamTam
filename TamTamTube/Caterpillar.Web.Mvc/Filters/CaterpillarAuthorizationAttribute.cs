using Caterpillar.Core.Application;
using Caterpillar.Core.Common;
using Caterpillar.Core.Extensions;
using Caterpillar.Core.Security;
using Caterpillar.Core.Session;
using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Caterpillar.Web.Mvc.Filters
{
    public class CaterpillarAuthorizationAttribute : AuthorizeAttribute
    {
        private string[] AllowedRoles;

        public string RedirectUrlIfNotAuthenticated { get; set; }

        public string RedirecUrlIfNotAuthorized { get; set; }

        public CaterpillarAuthorizationAttribute(params string[] roles)
            : base()
        {
            AllowedRoles = roles;
            base.Roles = string.Join(",", roles);

            string cultureInfo = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            this.RedirectUrlIfNotAuthenticated = string.Format("~{0}/Home", cultureInfo);
            this.RedirecUrlIfNotAuthorized = this.RedirectUrlIfNotAuthenticated;
        }

        //protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        //{
        //    bool authorize = false;
        //    foreach (var role in AllowedRoles)
        //    {
        //        var user = context.AppUser.Where(m => m.UserID == GetUser.CurrentUser/* getting user form current context */ && m.Role == role &&
        //        m.IsActive == true); // checking active users with allowed roles.  
        //        if (user.Count() > 0)
        //        {
        //            authorize = true; /* return true if Entity has current user(active) with specific role */
        //        }
        //    }
        //    return authorize;
        //}

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpContextBase httpContext = filterContext.RequestContext.HttpContext;
            bool isAuthenticated = httpContext.Request.IsAuthenticated;

            if (!isAuthenticated)
            {
                isAuthenticated = SessionIsValid();
            }

            string appPath = httpContext.Request.ApplicationPath;
            if (!appPath.EndsWith("/"))
            {
                appPath = appPath + "/";
            }
            if (!isAuthenticated)
            {
                string loginPageUrl = this.RedirectUrlIfNotAuthenticated;
                if (string.IsNullOrWhiteSpace(loginPageUrl))
                {
                    loginPageUrl = GetLoginUrlFromFormsAuthConfig();
                }
                loginPageUrl += string.Format("?redirectUrl={0}", httpContext.Request.Url.ToString());
                loginPageUrl = appPath + loginPageUrl.TrimStart('~').TrimStart('/');
                filterContext.Result = GetRedirectContent(loginPageUrl);
                return;
            }

            // Eğer controllerda role belirtilmediyse giriş izni var demektir.
            bool hasRole = true;
            if (this.AllowedRoles != null && this.AllowedRoles.Length > 0)
            {
                hasRole = false;
                foreach (string eachRole in this.AllowedRoles)
                {
                    if (!hasRole)
                    {
                        hasRole = this.CurrentUser.IsInRole(eachRole);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (!hasRole)
            {
                string authorizedUrl = this.RedirecUrlIfNotAuthorized;
                authorizedUrl += string.Format("?redirectUrl={0}", httpContext.Request.Url.ToString());
                authorizedUrl = appPath + authorizedUrl.TrimStart('~').TrimStart('/');
                filterContext.Result = GetRedirectContent(authorizedUrl);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult("Oturumunuz sonlanmıştır. Lütfen tekrar oturum açınız.");
        }

        protected virtual CaterpillarPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User as CaterpillarPrincipal;
            }
        }

        private bool SessionIsValid()
        {
            bool result = false;
            this.RunSafely(() =>
            {
                ISessionService sessionService = ApplicationFoundation.Current.SessionService;
                BasicSessionObject session = sessionService.Get<BasicSessionObject>(Constants.Session.BasicSessionObject);
                if (session != null)
                {
                    result = session.UserId != Guid.Empty;
                }
            });
            return result;
        }

        private string GetLoginUrlFromFormsAuthConfig()
        {
            string loginUrl = "~/Account/Login";
            AuthenticationSection authSection = null;
            string appPath = HttpContext.Current.Request.ApplicationPath;
            var webConfig = WebConfigurationManager.OpenWebConfiguration(appPath);
            if (webConfig.SectionGroups != null)
            {
                var webSection = webConfig.SectionGroups.Get("system.web");
                if (webSection != null)
                {
                    authSection = (AuthenticationSection)webSection.Sections.Get("authentication");
                    if (authSection != null)
                    {
                        loginUrl = authSection.Forms.LoginUrl;
                    }
                }
            }
            return loginUrl;
        }

        private ContentResult GetRedirectContent(string redirectUrl)
        {
            string script = string.Format("<script>this.window.location.href = '{0}';</script>", redirectUrl);
            ContentResult content = new ContentResult();
            content.Content = script;
            return content;
        }
    }
}
