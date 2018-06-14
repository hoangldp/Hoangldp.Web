using System.Web;
using System.Web.Mvc;

namespace Hoangldp.Web.Framework.Extenstion
{
    public static class AuthorizationContextExtenstion
    {
        public static ActionResult GetLogin(this AuthorizationContext context, string link, bool markUnauthorized = false)
        {
            string urlLogin = $"{link}";
            if (string.IsNullOrEmpty(link)) new RedirectResult("");

            if (urlLogin.StartsWith("~"))
            {
                urlLogin = urlLogin.Replace("~", "");
            }

            if (!string.IsNullOrEmpty(context.RequestContext.HttpContext.Request.RawUrl))
            {
                urlLogin += $"?returnurl={HttpUtility.UrlEncode(context.RequestContext.HttpContext.Request.RawUrl)}&unauthorized=1";
                if (markUnauthorized)
                {
                    urlLogin += "&unauthorized=1";
                }
            }
            return new RedirectResult(urlLogin);
        }
    }
}
