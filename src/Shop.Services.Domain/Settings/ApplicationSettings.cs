namespace Shop.Services.Domain.Settings
{
    public class ApplicationSettings
    {
        public ApplicationSettings(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }

        public string ConnectionStringName { get; private set; }
    }
}