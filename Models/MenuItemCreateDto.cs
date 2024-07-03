using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class MenuItemCreateDto
    {

        [Required]
        public String? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; } = String.Empty;
    }
}
