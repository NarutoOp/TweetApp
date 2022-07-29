namespace TweetApp.Domain.Interfaces
{
    /// <summary>
    /// IDatabaseSettings Interface
    /// </summary>
    public interface IDatabaseSettings
    {
        /// <summary>
        /// CollectionName get or set
        /// </summary>
        string CollectionName { get; set; }

        /// <summary>
        /// ConnectionString get or set
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// DatabaseName get or sets
        /// </summary>
        string DatabaseName { get; set; }
    }
}
