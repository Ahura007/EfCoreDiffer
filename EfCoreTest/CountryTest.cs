using System.Collections.Generic;
using System.Linq;
using EfCore;
using EfCore.Controllers;
using EfCore.Model;
using EfCore.Service;
using EfCoreTest.Initialize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EfCoreTest
{
    public class CountryTest : IClassFixture<TestStartup<Startup>>
    {
        public CountryTest(TestStartup<Startup> factory)
        {
            Factory = factory;
        }

        public TestStartup<Startup> Factory;
 


        [Fact]
        public void GetAll()
        {
            using (var scope = Factory.Services.CreateScope())
            {
                var countryMock = new Mock<ICountryService>();
                countryMock.Setup(x => x.GetAll()).Returns(InMemoryDb.OnlyCountries);
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<CountryController>>();
                var countryController = new CountryController(logger, countryMock.Object);

                var result = countryController.GetAll();

                var model = Assert.IsAssignableFrom<IEnumerable<Country>>(result);
                Assert.Equal(InMemoryDb.OnlyCountries().Count, model.Count());

            }
        }
    }
}