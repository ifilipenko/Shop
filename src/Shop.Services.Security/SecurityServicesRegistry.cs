using StructureMap.Configuration.DSL;

namespace Shop.Services.Security
{
    public class SecurityServicesRegistry : Registry
    {
        public SecurityServicesRegistry()
        {
            Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
        }
    }
}