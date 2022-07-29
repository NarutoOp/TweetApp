namespace TweetApp.Domain.Models.ExceptionModels
{
    /// <summary>
    /// Contains information about an error
    /// </summary>
    public class Error
    {
        /// <summary>
        /// A detailed description of an error.
        /// </summary>
        public string Message { get; set; }

        public Error(string message)
        {
            Message = message;
        }
    }
}
