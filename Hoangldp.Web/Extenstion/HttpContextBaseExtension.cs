using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace Hoangldp.Web.Framework.Extenstion
{
    public static class HttpContextBaseExtension
    {
        private static readonly string[] Purposes = { "key" }; // Todo: read in config

        public static string GetCookie(this HttpContextBase httpContext, string key)
        {
            HttpCookie cookie = httpContext.Request.Cookies[key];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                var base64 = Convert.FromBase64String(cookie.Value);
                var decrypt = MachineKey.Unprotect(base64, Purposes);
                return Encoding.UTF8.GetString(decrypt ?? throw new InvalidOperationException());
            }

            return null;
        }

        public static void SetCookie(this HttpContextBase httpContext, string key, string value, DateTime? expires = null, bool httpOnly = true, bool secure = false)
        {
            var encrypt = MachineKey.Protect(Encoding.UTF8.GetBytes(value), Purposes);
            string base64Text = Convert.ToBase64String(encrypt);

            HttpCookie cookie = new HttpCookie(key, base64Text);
            cookie.HttpOnly = httpOnly;
            cookie.Secure = secure;
            if (expires != null)
            {
                cookie.Expires = expires.Value;
            }
            httpContext.Response.Cookies.Add(cookie);
        }

        public static void ClearCookie(this HttpContextBase httpContext, string key, bool multiple = false)
        {
            List<string> listKeys = new List<string>();
            if (multiple)
            {
                listKeys = httpContext.Request.Cookies.AllKeys.Where(k => Regex.IsMatch(k, key)).ToList();
            }
            else
            {
                listKeys.Add(key);
            }

            foreach (string itemKey in listKeys)
            {
                HttpCookie cookie = new HttpCookie(itemKey, string.Empty);
                cookie.Expires = DateTime.Now.AddYears(-1);
                httpContext.Response.Cookies.Add(cookie);
            }
        }
    }
}
