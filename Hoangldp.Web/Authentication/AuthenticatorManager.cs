using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Hoangldp.Web.Extenstion;

namespace Hoangldp.Web.Authentication
{
    public class AuthenticatorManager<TUser> : IAuthenticatorManager<TUser>
        where TUser : class, IUser
    {
        private const string KeyCookie = "_UserLogin";

        protected IUserLoginService<TUser> UserLoginService { get; }

        public AuthenticatorManager(IUserLoginService<TUser> userLoginService)
        {
            UserLoginService = userLoginService;
        }

        protected virtual int GetMinutesExpireCookie()
        {
            return 1 * 24 * 60;
        }

        private string GetKeyCookie(bool all = false)
        {
            if (all) return "^" + KeyCookie + "_\\w{32}$";
            Type type = GetType();
            byte[] name = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(type.FullName ?? throw new InvalidOperationException()));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                sb.Append(name[i].ToString("x2"));
            }
            return KeyCookie + "_" + sb;
        }

        public string GetUsername()
        {
            return new HttpContextWrapper(HttpContext.Current).GetCookie(GetKeyCookie());
        }

        public UserLoginState Login(string username, string password, bool remember = false)
        {
            try
            {
                TUser user = UserLoginService.GetByUsername(username);
                if (user != null)
                {
                    IUserLoginPasswordService<TUser> passwordService = (IUserLoginPasswordService<TUser>)UserLoginService;
                    passwordService.SetPasswordHash(user, user.Password);
                    if (passwordService.VerifiPassword(user, password))
                    {
                        DateTime? expires = null;
                        if (remember)
                        {
                            expires = DateTime.Now.AddMinutes(GetMinutesExpireCookie());
                        }
                        new HttpContextWrapper(HttpContext.Current).SetCookie(GetKeyCookie(), username, expires);
                        return UserLoginState.Success;
                    }
                }
            }
            catch
            {
                return UserLoginState.Fail;
            }
            return UserLoginState.Fail;
        }

        public void Logout(bool all = false)
        {
            new HttpContextWrapper(HttpContext.Current).ClearCookie(GetKeyCookie(all), all);
        }

        public TUser GetUserLogin()
        {
            string username = GetUsername();
            if (!string.IsNullOrEmpty(username))
            {
                return UserLoginService.GetByUsername(username);
            }

            return null;
        }
    }
}