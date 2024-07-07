using RestaurantReservationAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Models
{
    public class EmployeeForUpdateDto
    {

        [MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;

        public EmployeeRole Role { get; set; }
    }
}
