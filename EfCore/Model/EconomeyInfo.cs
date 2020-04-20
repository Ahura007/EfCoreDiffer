using System.ComponentModel.DataAnnotations;

namespace EfCore.Model
{
    public class EconomyInfo
    {
        [Key]
        public int Id { get; set; }
        public string AverageWeeklyHours { get; set; }
        public string VendorPerformance { get; set; }
        public string BuildingPermits { get; set; }
    }
}