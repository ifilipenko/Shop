using System.Web.Security;

namespace Shop.Services.Infrastructure
{
    public class AuthenticationService : IAuthenticationService
    {
        public void CreateUser(string username, string password)
        {
            Membership.CreateUser(username, password);
        }
    }
}