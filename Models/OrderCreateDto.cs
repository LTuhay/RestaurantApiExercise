using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class OrderCreateDto
    {

        [Required]
        public int EmployeeId { get; set; }
    }
}
