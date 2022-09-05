namespace TweetApp.Domain.Validators
{
    using FluentValidation;
    using TweetApp.Domain.Models.Tweet;

    /// <summary>
    /// TweetValidator Class
    /// </summary>
    public class TweetMessageValidator : AbstractValidator<TweetMessage>
    {
        public TweetMessageValidator(TweetMessage tweetMessage)
        {
            RuleFor(x => x.Message)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Tweet Reply cannot be blank.")
                .Length(0, 144)
                .WithMessage("Tweet Message cannot be more than 144 characters.");
        }
    }
}
