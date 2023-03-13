using HealthCare.Abstraction;
using HealthCare.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class Clinic : AuditableEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The maximum length is 100 characters")]
        public string Name { get; set; } = String.Empty;
        public Address Address { get; set; } = new();
        public ICollection<Doctor>? Doctors { get; set; }
        public List<ClinicDoctor>? ClinicDoctors { get; set; }
    }
}
