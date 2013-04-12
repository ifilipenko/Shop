using System.Web.Security;

namespace Shop.Services.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        public void SignIn(string username, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(username, rememberMe);
        }
    }
}