using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class ClinicDoctor
    {
        public int? ClinicId { get; set; }
        [ScaffoldColumn(false)]
        public Clinic? Clinic { get; set; }
        public int? DoctorId { get; set; }
        [ScaffoldColumn(false)]
        public Doctor? Doctor { get; set; }
    }
}
