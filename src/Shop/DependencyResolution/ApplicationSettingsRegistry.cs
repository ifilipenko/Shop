using System.Configuration;
using Shop.Services.Domain.Settings;
using StructureMap.Configuration.DSL;

namespace Shop.DependencyResolution
{
    public class ApplicationSettingsRegistry : Registry
    {
        public ApplicationSettingsRegistry()
        {
            For<ApplicationSettings>().Singleton().Use(LoadApplicationSettings);
        }

        private ApplicationSettings LoadApplicationSettings()
        {
            return new ApplicationSettings(ConfigurationManager.ConnectionStrings["ShopDB"].ConnectionString);
        }
    }
}