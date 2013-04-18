using StructureMap.Configuration.DSL;

namespace Shop.Services.Domain
{
    public class DomainServicesRegistry : Registry
    {
        public DomainServicesRegistry()
        {
            Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
        }
    }
}