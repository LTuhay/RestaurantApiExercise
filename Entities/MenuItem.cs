using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Entities
{
    public class MenuItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuItemId { get; set; }

        [Required]
        [MaxLength(30)]
        public String? Name { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string Description { get; set; } = String.Empty;
    }
}