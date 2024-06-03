
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestaurantReservationAPI.Entities;

namespace RestaurantReservationAPI.Models
{
    public class EmployeeDto
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public EmployeeRole Role { get; set; }

    }
}
