using BloodLink.Application.Commands.RegisterUser;
using FluentValidation;

namespace BloodLink.Application.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage("FullName is required.")
                .MaximumLength(150).WithMessage("FullName must not exceed 150 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.")
                .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must have at least 6 characters.");

            RuleFor(u => u.Role)
                .IsInEnum().WithMessage("Invalid user role.");
        }
    }
}
