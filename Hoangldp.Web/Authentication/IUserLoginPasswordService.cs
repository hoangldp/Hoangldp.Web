namespace Hoangldp.Core.Web.Authentication
{
    public interface IUserLoginPasswordService<TUser>
        where TUser : class, IUserLogin
    {
        string GetPasswordHash(TUser user);
        void SetPasswordHash(TUser user, string passwordHash);
        IPasswordHash GetHasher();
        bool VerifiPassword(TUser user, string password);
    }
}