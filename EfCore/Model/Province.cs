using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EfCore.Model
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public EconomyInfo EconomyInfo { get; set; }
        
    }
}