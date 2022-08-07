namespace TweetApp.Domain.Models.ExceptionModels
{
    /// <summary>
    /// ErrorInfo Class
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Errors list
        /// </summary>
        public List<Error> Info { get; set; }

        public ErrorInfo(string message, List<Error> info = null)
        {
            Message = message;
            if (info != null)
            {
                Info = info;
            }
        }
    }
}
