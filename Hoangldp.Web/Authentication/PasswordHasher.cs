using System.Web.Helpers;

namespace Hoangldp.Web.Authentication
{
    public class PasswordHasher : IPasswordHash
    {
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }
    }
}