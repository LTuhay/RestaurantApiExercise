using RestaurantReservationAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class ReservationDto
    {
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public CustomerDto Customer { get; set; }
        public List<OrderDto>? Orders { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int NumberOfGuests { get; set; }
        public String Notes { get; set; } = String.Empty;


    }
}
