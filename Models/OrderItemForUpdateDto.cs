using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class OrderItemForUpdateDto
    {
        [Required]
        public int MenuItemId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
