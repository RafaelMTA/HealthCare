using HealthCare.Abstraction;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class Doctor : AuditableEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The maximum length is 100 characters")]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = String.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "The maximum length is 100 characters")]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = String.Empty;
        [ScaffoldColumn(false)]
        [DisplayName("Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public Speciality? Speciality { get; set; }
        public int SpecialityId { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Speciality")]
        public string? SpecialityName { get { return Speciality != null ? Speciality.Name : ""; } }

        public ICollection<Clinic>? Clinics { get; set; }
        public List<ClinicDoctor>? ClinicDoctors { get; set; }
    }
}
