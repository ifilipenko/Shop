using StructureMap.Configuration.DSL;

namespace Shop.Services.Security
{
    public class SecurityServicesRegistry : Registry
    {
        public SecurityServicesRegistry()
        {
            For<IAccountService>().Use<AccountService>();
        }
    }
}