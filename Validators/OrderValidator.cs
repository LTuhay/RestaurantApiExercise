using FluentValidation;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Validators
{
    public class OrderValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderValidator()
        {
            RuleFor(o => o.EmployeeId)
                .NotEmpty().WithMessage("Employee Id is required.");
        }
    }

}
