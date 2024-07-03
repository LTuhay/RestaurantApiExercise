using FluentValidation;
using RestaurantReservationAPI.Models;

namespace RestaurantReservationAPI.Validators
{
    public class MenuItemValidator : AbstractValidator<MenuItemCreateDto>
    {
        public MenuItemValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Name cannot exceed 30 characters.");

            RuleFor(m => m.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(m => m.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
        }
    }
}
