namespace TweetApp.Domain.Validators
{
    using FluentValidation;
    using TweetApp.Domain.Models.Tweet;

    /// <summary>
    /// TweetValidator Class
    /// </summary>
    public class TweetValidator : AbstractValidator<Tweet>
    {
        public TweetValidator(Tweet tweet)
        {
            RuleFor(x => x.TweetMessage.Message)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Tweet Message cannot be blank.")
                .Length(0, 144)
                .WithMessage("Tweet Message cannot be more than 144 characters.");
        }
    }
}
