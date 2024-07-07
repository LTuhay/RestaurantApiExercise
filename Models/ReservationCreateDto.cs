using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class ReservationCreateDto
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }
        public String Notes { get; set; } = String.Empty;
    }
}
