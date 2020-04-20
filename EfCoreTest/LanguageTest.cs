using EfCore;
using EfCore.Controllers;
using EfCore.Model;
using EfCore.Service;
using EfCoreTest.Initialize;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace EfCoreTest
{
    public class LanguageTest : IClassFixture<TestStartup<Startup>>
    {
        public LanguageTest(TestStartup<Startup> factory)
        {
            Factory = factory;
        }

        public TestStartup<Startup> Factory;


        [Fact]
        public void GetById()
        {
            using (var scope = Factory.Services.CreateScope())
            {
                var language = new Language() {Culture = "dj-af", Languages = "djibouti"};
                var languageService = scope.ServiceProvider.GetRequiredService<ILanguageService>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<LanguageController>>();
                var languageController = new LanguageController(logger, languageService);
              
                var result = languageController.GetById(1);
 
                Assert.Equal(language.Languages, result.Languages);
            }
        }
    }
}