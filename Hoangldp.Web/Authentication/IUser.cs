namespace Hoangldp.Web.Authentication
{
    public interface IUser : IUserLogin
    {
        string Password { get; set; }
    }
}