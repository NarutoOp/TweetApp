namespace TweetApp.Domain.Validators
{
    using FluentValidation;
    using TweetApp.Domain.Exceptions;
    using TweetApp.Domain.Models.ExceptionModels;

    /// <summary>
    /// ParseError class
    /// </summary>
    public static class Validations
    {
        /// <summary>
        /// Check if request is valid
        /// </summary>
        /// <typeparam name="TRequest">TRequest instance</typeparam>
        /// <param name="request">TRequest instance</param>
        /// <param name="validator">IValidator instance</param>
        public static void EnsureValid<TRequest>(TRequest request, IValidator<TRequest> validator)
        {
            var validationError = new DomainException("Invalid Request", System.Net.HttpStatusCode.BadRequest);
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                validationError.Errors.AddRange(
                    validationResult.Errors.Select(
                        validationFailure => new Error(validationFailure.ErrorMessage)
                        ));
                throw validationError;
            }
        }

    }
}
