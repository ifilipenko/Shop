namespace Shop.Services.Security
{
    public interface IAuthenticationService
    {
        void SignIn(string username, string password, bool rememberMe);
    }
}