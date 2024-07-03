using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class ReservationForUpdateDto
    {

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public List<int>? OrdersId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int NumberOfGuests { get; set; }
        public String Notes { get; set; } = String.Empty;
    }
}
