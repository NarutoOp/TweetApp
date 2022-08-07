namespace TweetApp.Domain.Validators
{
    using FluentValidation;
    using TweetApp.Domain.Models.Users;

    /// <summary>
    /// UserRequestValidator class
    /// </summary>
    public class UserRequestValidator: AbstractValidator<User>
    {
        public UserRequestValidator(User user)
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("First Name cannot be blank.")
                .Length(0, 100)
                .WithMessage("First Name cannot be more than 100 characters.")
                .Must(val => FieldValidator.isValidAlphabet(val))
                .WithMessage("First Name should have alphabet letter only");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last Name cannot be blank.")
                .Length(0, 100)
                .WithMessage("Last Name cannot be more than 100 characters.")
                .Must(val => FieldValidator.isValidAlphabet(val))
                .WithMessage("Last Name should have alphabet letter only");

            RuleFor(x => x.LoginId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("LoginId cannot be blank.")
                .Length(0, 10)
                .WithMessage("LoginId cannot be more than 10 characters.")
                .Must(val => FieldValidator.isValidAlphaNumeric(val))
                .WithMessage("LoginId should be alphanumeric only");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email cannot be blank.")
                .EmailAddress()
                .WithMessage("Email should be valid.");

            RuleFor(x => x.ContactNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Contact Number cannot be blank.")
                .Length(10)
                .WithMessage("Contact Number should contain 10 digits only.")
                .Must(val => FieldValidator.isValidNumeric(val))
                .WithMessage("Contact Number should be numbers only");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password cannot be blank.");

            RuleFor(x => x.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("ConfirmPassword cannot be blank.");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });

        }
    }
}
