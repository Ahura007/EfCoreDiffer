using System.ComponentModel.DataAnnotations;

namespace EfCore.Model
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Culture { get; set; }
        public string Languages { get; set; }

    }
}