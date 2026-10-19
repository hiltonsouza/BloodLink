using BloodLink.Application.Commands.UpdateDonor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Application.Validators
{
    public class UpdateDonorCommandValidator : AbstractValidator<UpdateDonorCommand>
    {
        public UpdateDonorCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Donor ID is required.")
                .GreaterThan(0).WithMessage("Donor ID must be greater than zero.");
            RuleFor(x => x.FullName)               
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");
            RuleFor(x => x.Weight)
                .GreaterThan(49).WithMessage("Weight must be greater than zero.");
            RuleFor(x => x.BloodType)
                .Must(bt =>
                    new[] { "A", "B", "AB", "O" }.Contains(bt))
                .When(x => !string.IsNullOrEmpty(x.BloodType));
            RuleFor(x => x.RhFactor)
                .Must(rh => new[] { "+", "-" }.Contains(rh))
                .When(x => !string.IsNullOrEmpty(x.RhFactor))
                .WithMessage("Rh factor must be either '+' or '-'.");
        }
    }
}
