using System.ComponentModel.DataAnnotations;

namespace HealthCare.Abstraction
{
    public abstract class AuditableEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public string? Created_By { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? Created_At { get; set; }
        [ScaffoldColumn(false)]
        public string? Updated_By { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? Updated_At { get; set; }
    }
}
