using HealthCare.Abstraction;
using HealthCare.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.Models
{
    public class Insurance : AuditableEntity
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - hh:00 tt}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; } = new DateTime();
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - hh:00 tt}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; } = new DateTime();
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        [ScaffoldColumn(false)]
        [DisplayName("Total Price")]
        [DataType(DataType.Currency)]
        public double TotalPrice { 
            get {
                var days = (End - Start).TotalDays;
                if (days > 0 && Product != null) return days * Product!.Price;
                return 0;
            } 
        }
    }
}
