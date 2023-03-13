using Microsoft.EntityFrameworkCore;

namespace HealthCare.ValueObjects
{
    [Owned]
    public class CoverageTime
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
