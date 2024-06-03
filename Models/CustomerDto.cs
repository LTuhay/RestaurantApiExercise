using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class CustomerDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;

    }
}
