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
        ///  User Collection Name
        /// </summary>
        protected string _userCollectionName;

        /// <summary>
        ///  Tweet Collection Name
        /// </summary>
        protected string _tweetCollectionName;

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
            _userCollectionName = appsettings.GetSection("DatabaseSettings")["UserCollectionName"];
            _tweetCollectionName = appsettings.GetSection("DatabaseSettings")["TweetCollectionName"];
        }

    }
}