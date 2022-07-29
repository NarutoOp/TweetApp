namespace TweetApp.DAL
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Configuration class
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Connection String
        /// </summary>
        protected string _connectionString;

        /// <summary>
        /// Database Name
        /// </summary>
        protected string _databaseName;

        /// <summary>
        ///  Collection
        /// </summary>
        protected string _collectionName;

        /// <summary>
        /// Configuration contructor
        /// </summary>
        public Configuration()
        {
            var appsettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = appsettings.GetSection("DatabaseSettings")["ConnectionString"];
            _databaseName = appsettings.GetSection("DatabaseSettings")["DatabaseName"];
            _collectionName = appsettings.GetSection("DatabaseSettings")["CollectionName"];
        }

    }
}