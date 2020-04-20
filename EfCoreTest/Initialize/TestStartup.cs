using System;
using System.Linq;
using System.Runtime.InteropServices;
using EfCore.Context;
using EfCore.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EfCoreTest.Initialize
{
    public class TestStartup<T> : WebApplicationFactory<T> where T : class
    {
        public IHost HostWeb { get; set; }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            HostWeb = builder.Build();
            HostWeb.Start();
            return HostWeb;
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
            {
                var path = hostingContext.HostingEnvironment.ContentRootPath;
                configurationBuilder.AddJsonFile($"{path}\\appsettings.json", true, true);
                configurationBuilder.AddEnvironmentVariables();
            });

            builder.ConfigureServices(services =>
            {
                var removeDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (removeDbContext != null)
                {
                    services.Remove(removeDbContext);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                    db.Database.EnsureCreated();

                    try
                    {
                        InMemoryDb.Build(db);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

            });
        }
    }
}