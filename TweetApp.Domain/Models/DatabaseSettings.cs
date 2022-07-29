namespace TweetApp.Domain.Models
{
    using TweetApp.Domain.Interfaces;

    /// <summary>
    /// DatabaseSettings class
    /// </summary>
    public class DatabaseSettings : IDatabaseSettings
    {
        /// <summary>
        /// CollectionName get or set
        /// </summary>
        public string CollectionName { get; set; } = string.Empty;

        /// <summary>
        /// ConnectionString get or set
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// DatabaseName get or sets
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;
    }
}
