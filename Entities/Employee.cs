using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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