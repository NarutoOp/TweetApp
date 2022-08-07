namespace TweetApp.Domain.Validators
{
    using FluentValidation;
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// LoginValidator class
    /// </summary>
    public class LoginValidator : AbstractValidator<UserLogin>
    {
        public LoginValidator(UserLogin userLogin)
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("UserName cannot be blank.")
                .Length(0, 10)
                .WithMessage("User Name cannot be more than 10 characters.")
                .Must(val => FieldValidator.isValidAlphaNumeric(val))
                .WithMessage("User Name is alphanumeric");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password cannot be blank.")
                .Length(6, 100)
                .WithMessage("Password cannot be less then 6 characters.");
        }
    }
}
