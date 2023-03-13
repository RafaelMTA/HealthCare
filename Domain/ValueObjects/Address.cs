using Microsoft.EntityFrameworkCore;

namespace HealthCare.ValueObjects
{
    [Owned]
    public class Address
    {
        public string Street { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Suburb { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string PostCode { get; set; } = String.Empty;
    }
}
