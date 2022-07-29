namespace TweetApp.Domain.Exceptions
{
    using System.Net;
    using TweetApp.Domain.Models.ExceptionModels;

    /// <summary>
    /// Domain Exception classs
    /// </summary>
    public class DomainException: Exception
    {
        public string ErrorMessage { get; }
        public HttpStatusCode HttpStatusCode { get; }

        public List<Error> Errors { get; } = new List<Error>();

        public DomainException(string message, HttpStatusCode httpStatusCode, List<Error> errors = null): base(message)
        {
            this.ErrorMessage = message;
            this.HttpStatusCode = httpStatusCode;
            if (errors != null)
            {
                Errors.AddRange(errors);
            }
        }
    }
}
