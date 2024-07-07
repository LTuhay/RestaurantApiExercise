using FluentValidation;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("First name can only contain letters and spaces.")
                .MaximumLength(15).WithMessage("First name cannot exceed 15 characters.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Last name can only contain letters and spaces.")
                .MaximumLength(15).WithMessage("Last name cannot exceed 15 characters.");


        }

    }
}
