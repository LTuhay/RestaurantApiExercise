using FluentValidation;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("First name can only contain letters and spaces.")
                .MaximumLength(15).WithMessage("First name cannot exceed 15 characters.");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Last name can only contain letters and spaces.")
                .MaximumLength(15).WithMessage("Last name cannot exceed 15 characters.");

            RuleFor(e => e.Role)
                .NotEmpty().WithMessage("Role is required.");
        }
    }
}
