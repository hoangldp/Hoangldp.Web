namespace Hoangldp.Core.Web.Authentication
{
    public interface IPasswordHash
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}