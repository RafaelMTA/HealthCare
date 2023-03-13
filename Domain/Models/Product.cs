using HealthCare.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product : AuditableEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The maximum character is 100")]
        public string Name { get; set; } = String.Empty;
        [StringLength(200, ErrorMessage = "The maximum character is 200")]
        public string Description { get; set; } = String.Empty;
        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; } = String.Empty;
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
