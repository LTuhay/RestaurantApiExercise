using RestaurantReservationAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class OrderDto
    {
        [Required]
        public int OrderId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        [Required]
        public EmployeeDto Employee { get; set; }

        public decimal TotalAmount => OrderItems.Sum(item => item.Quantity * item.MenuItem.Price);


    }
}
