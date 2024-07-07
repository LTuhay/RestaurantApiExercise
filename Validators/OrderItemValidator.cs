using FluentValidation;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItemCreateDto>
    {
        public OrderItemValidator()
        {
            RuleFor(o => o.MenuItemId)
                .NotEmpty().WithMessage("Menu Item Id is required.");

            RuleFor(o => o.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }

}
