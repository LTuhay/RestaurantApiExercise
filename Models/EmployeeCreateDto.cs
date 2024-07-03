using RestaurantReservationAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class EmployeeCreateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public EmployeeRole Role { get; set; }
    }
}
