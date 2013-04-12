using System.Web.Security;

namespace Shop.Services.Security
{
    public class AccountService : IAccountService
    {
        public void RegisterNewUser(string userName, string password)
        {
            Membership.CreateUser(userName, password);
        }
    }
}