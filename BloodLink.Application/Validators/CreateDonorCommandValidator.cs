using BloodLink.Application.Commands.CreateDonor;
using FluentValidation;

namespace BloodLink.Application.Validators
{
    public class CreateDonorCommandValidator : AbstractValidator<CreateDonorCommand>
    {
        public CreateDonorCommandValidator()
        {
            RuleFor(p => p.FullName)
                .NotNull().NotEmpty().WithMessage("FullName is required")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("E-mail is not valid!");

            RuleFor(p => p.BirthDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Birth Date is required");

            RuleFor(p => p.Gender)
                .NotNull()
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => g == "Male" || g == "Female").WithMessage("Gender must be either 'Male' or 'Female'");

            RuleFor(p => p.Weight)
                .NotNull()
                .NotEmpty().WithMessage("Weight is required")
                .GreaterThan(49).WithMessage("Wight must be at least 50 kg");

            RuleFor(p => p.BloodType)
           .NotEmpty().WithMessage("Blood type is required")
           .Must(bloodType => new[] { "A", "B", "AB", "O" }.Contains(bloodType))
           .WithMessage("Invalid blood type");

            RuleFor(p => p.RhFactor)
            .Must((command, rh) => (rh == "+" || rh == "-"))
            .WithMessage("Rh Factor must be '+' or '-'.");

            RuleFor(p => p.ZipCode)
            .NotEmpty().WithMessage("Zip Code is required")
            .Matches(@"^\d{8}$").WithMessage("Zip Code must be an 8-digit in this format 00000000");
        }
    }
}
