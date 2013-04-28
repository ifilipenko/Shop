namespace Shop.Services.Infrastructure
{
    public interface IAuthenticationService
    {
        void CreateUser(string username, string password);
        void Authenticate(string username, string password, bool rememberMe);
    }
}