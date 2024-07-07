using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class MenuItemDto
    {

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        [MaxLength(30)]
        public String? Name { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string Description { get; set; } = String.Empty;

    }
}
