namespace Shop.Services.Security
{
    public interface IAuthenticationService
    {
        void SignIn(string username, bool rememberMe);
    }
}