namespace Shop.Services.Infrastructure
{
    public interface IAuthenticationService
    {
        void CreateUser(string username, string password);
    }
}