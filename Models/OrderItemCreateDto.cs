using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class OrderItemCreateDto
    {
        [Required]
        public int MenuItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
