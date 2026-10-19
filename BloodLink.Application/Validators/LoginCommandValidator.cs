using BloodLink.Application.Commands.Login;
using FluentValidation;

namespace BloodLink.Application.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
