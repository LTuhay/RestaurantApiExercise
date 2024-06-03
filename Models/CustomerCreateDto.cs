using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class CustomerCreateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

    }
}
