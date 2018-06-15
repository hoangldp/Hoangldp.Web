namespace Hoangldp.Web.Authentication
{
    public interface IUserLoginService<TUser> : IUserLoginPasswordService<TUser>
        where TUser : class, IUserLogin
    {
        TUser GetByUsername(string username);
    }
}