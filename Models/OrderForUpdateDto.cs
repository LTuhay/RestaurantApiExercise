using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class OrderForUpdateDto
    {
        [Required]
        public int EmployeeId { get; set; }
    }
}
