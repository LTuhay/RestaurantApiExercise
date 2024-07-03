using RestaurantReservationAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class OrderItemDto
    {
        [Required]
        public int OrderItemId { get; set; }

        [Required]
        public MenuItemDto MenuItem { get; set; }

        [Required]
        [Range (0, int.MaxValue)]
        public int Quantity { get; set; }

    }
}
