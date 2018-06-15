using Hoangldp.Web.Extenstion;
using System;
using System.Web;
using System.Web.Mvc;

namespace Hoangldp.Web.Attributes
{
    public abstract class AuthorizeBaseAttribute : AuthorizeAttribute
    {
        protected abstract object GetUserLogin();

        protected abstract void Logout();

        protected virtual string GetUrlLogin()
        {
            return "/login";
        }

        /// <summary>When overridden, provides an entry point for custom authorization checks.</summary>
        /// <returns>true if the user is authorized; otherwise, false.</returns>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="httpContext" /> parameter is null.</exception>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return GetUserLogin() != null;
        }

        /// <summary>Called when a process requests authorization.</summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="filterContext" /> parameter is null.</exception>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException(nameof(filterContext));
            if (!filterContext.IsChildAction) base.OnAuthorization(filterContext);
        }

        /// <summary>Processes HTTP requests that fail authorization.</summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            Logout();
            filterContext.Result = filterContext.GetLogin(GetUrlLogin());
        }
    }
}
