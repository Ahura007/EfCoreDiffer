using System.Collections.Generic;
using System.Linq;
using EfCore.Context;
using EfCore.Model;

namespace EfCoreTest.Initialize
{
    public class InMemoryDb
    {
        public static void Build(ApplicationDbContext db)
        {
            var iran = new Country()
            {
                Language = new Language() { Culture  = "Fa-ir" , Languages = "Persian"},
                Area = "middle east" , 
                Name = "Iran",
                Population = "80m",
                PostalCode = "+98",
                Provinces = new List<Province>()
                {
                    new Province() {Name = "Hamedan" , EconomyInfo = new EconomyInfo(){ AverageWeeklyHours = "no eco", BuildingPermits = "no eco", VendorPerformance = "no eco" } },
                    new Province() {Name = "abadan" , EconomyInfo = new EconomyInfo(){ AverageWeeklyHours = "no eco1", BuildingPermits = "no eco1", VendorPerformance = "no eco1" } },
                    new Province() {Name = "bahar" , EconomyInfo = new EconomyInfo(){ AverageWeeklyHours = "no eco2", BuildingPermits = "no eco2", VendorPerformance = "no eco2" } }
                },
            };
            var djibouti = new Country()
            {
                Language = new Language() { Culture = "dj-af", Languages = "djibouti" },
                Area = "africa",
                Name = "Iran",
                Population = "1m",
                PostalCode = "none",
                Provinces = new List<Province>()
                {
                    new Province() {Name = "1" , EconomyInfo = new EconomyInfo(){ AverageWeeklyHours = "1", BuildingPermits = "1", VendorPerformance =  "1" } },
                    new Province() {Name = "2" , EconomyInfo = new EconomyInfo(){ AverageWeeklyHours = "2", BuildingPermits = "10", VendorPerformance = "11" } },
                    new Province() {Name = "3" , EconomyInfo = new EconomyInfo(){ AverageWeeklyHours = "3", BuildingPermits = "13", VendorPerformance = "12" } }
                },
            };

            db.Countries.Add(djibouti);
            db.Countries.Add(iran);

            db.Countries.AddRange(OnlyCountries());

            db.SaveChanges();
        }

        public static List<Country> OnlyCountries()
        {
            List<Country> countries = new List<Country>()
            {
                new Country(){Name = "A",Area = "A"},
                new Country(){Name = "B",Area = "A"},
                new Country(){Name = "C",Area = "A"},
                new Country(){Name = "D",Area = "A"},
            };
            return countries;
        }
    }
}