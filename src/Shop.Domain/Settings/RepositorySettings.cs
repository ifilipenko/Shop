namespace Shop.Domain.Settings
{
    public class RepositorySettings
    {
        public RepositorySettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }
    }
}