using HealthCare.Abstraction;
using HealthCare.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class Appointment : AuditableEntity
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - hh:00 tt}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? ClinicId { get; set; }
        public Clinic? Clinic { get; set; }
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
