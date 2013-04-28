using System;
using System.Security.Authentication;
using System.Web.Security;

namespace Shop.Services.Infrastructure
{
    public class AuthenticationService : IAuthenticationService
    {
        public void CreateUser(string username, string password)
        {
            Membership.CreateUser(username, password);
        }

        public void Authenticate(string username, string password, bool rememberMe)
        {
            if (!Membership.ValidateUser(username, password))
                throw new AuthenticationException("Неверное имя пользователя или пароль");
            FormsAuthentication.SetAuthCookie(username, rememberMe);
        }
    }
}