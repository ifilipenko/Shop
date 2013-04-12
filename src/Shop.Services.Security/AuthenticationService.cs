using System.Security.Authentication;
using System.Web.Security;

namespace Shop.Services.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        public void SignIn(string username, string password, bool rememberMe)
        {
            if (!Membership.ValidateUser(username, password))
            {
                throw new AuthenticationException("Некорректное имя пользователя или пароль");
            }
            FormsAuthentication.SetAuthCookie(username, rememberMe);
        }
    }
}