using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EfCore.Model
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Population { get; set; }
        public ICollection<Province> Provinces { get; set; }
        public Language Language { get; set; }

        public Country()
        {
            
        }

        public Country(string postalCode , string name , string area , string population)
        {
            PostalCode = postalCode;
            Name = name;
            Area = area;
            Population = population;
        }
    }
}