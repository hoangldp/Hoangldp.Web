namespace Hoangldp.Core.Web.Authentication
{
    public interface IAuthenticatorManager<TUser>
        where TUser : class, IUser
    {
        TUser GetUserLogin();
        string GetUsername();
        UserLoginState Login(string username, string password, bool remember = false);
        void Logout(bool all = false);
    }
}