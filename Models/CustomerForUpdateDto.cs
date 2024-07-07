using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class CustomerForUpdateDto
    {

        [MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;

    }
}
