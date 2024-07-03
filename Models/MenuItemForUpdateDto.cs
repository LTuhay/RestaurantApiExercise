using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class MenuItemForUpdateDto
    {

        [MaxLength(30)]
        public String? Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string Description { get; set; } = String.Empty;
    }
}
