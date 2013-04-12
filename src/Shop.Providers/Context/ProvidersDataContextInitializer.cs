using System.Data.Entity;
using System.Web.Security;

namespace Shop.Providers.Context
{
    public class ProvidersDataContextInitializer:DropCreateDatabaseAlways<ProvidersDataContext>
    {
        protected override void Seed(ProvidersDataContext context)
        {
            WebSecurity.Register("Demo", "123456", "demo@demo.com", true, "Demo", "Demo");
            Roles.CreateRole("Admin");
            Roles.AddUserToRole("Demo", "Admin");
        }
    }
}