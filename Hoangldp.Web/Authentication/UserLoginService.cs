namespace Hoangldp.Core.Web.Authentication
{
    public abstract class UserLoginService<TUser> : IUserLoginService<TUser>
        where TUser : class, IUser
    {
        public abstract TUser GetByUsername(string username);

        public virtual string GetPasswordHash(TUser user)
        {
            return user.Password;
        }

        public virtual void SetPasswordHash(TUser user, string passwordHash)
        {
            user.Password = passwordHash;
        }

        public virtual IPasswordHash GetHasher()
        {
            return new PasswordHasher();
        }

        public virtual bool VerifiPassword(TUser user, string password)
        {
            string passwordHasded = GetPasswordHash(user);
            IPasswordHash hasher = GetHasher();
            return hasher.VerifyHashedPassword(passwordHasded, password);
        }
    }
}