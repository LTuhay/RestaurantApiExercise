using FluentValidation;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Validators
{
    public class ReservationValidator : AbstractValidator<ReservationCreateDto>
    {
        public ReservationValidator()
        {
            RuleFor(r => r.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(date => date > DateTime.Now).WithMessage("Reservation date must be in the future.");

            RuleFor(r => r.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required.");

            RuleFor(r => r.NumberOfGuests)
                .NotEmpty().WithMessage("Number Of Guests is required.")
                .GreaterThan(0).WithMessage("NumberOfGuests must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("The number of guests cannot exceed 20.");

            RuleFor(r => r.Notes)
                .MaximumLength(255).WithMessage("Notes cannot exceed 255 characters.")
                .When(r => !string.IsNullOrEmpty(r.Notes));

        }
    }

}
