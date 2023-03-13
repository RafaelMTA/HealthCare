using HealthCare.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class Speciality : AuditableEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "the maximum character is 100")]
        public string Name { get; set; } = String.Empty;
        [Required]
        [StringLength(200, ErrorMessage = "the maximum character is 200")]
        public string Description { get; set; } = String.Empty;
    }
}
