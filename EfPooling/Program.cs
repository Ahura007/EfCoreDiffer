using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EfPooling
{

    class Program
    {
        static string cs = "Server=(localdb)\\mssqllocaldb;Database=TestPoolDBContext;Trusted_Connection=True;MultipleActiveResultSets=true";

        static void Main(string[] args)
        {
            RunWithoutContextPooling();
            RunWithContextPooling();
        }

        public static void RunWithoutContextPooling()
        {
            Console.WriteLine("\nRun Without ContextPooling");
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<BloggingContext>(c => c.UseSqlServer(cs))
                .BuildServiceProvider();

            new RunTests().Start(serviceProvider);
        }

        public static void RunWithContextPooling()
        {
            Console.WriteLine("\nRun With ContextPooling");
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .AddDbContextPool<BloggingContext>(c => c.UseSqlServer(cs), poolSize:128)
                .BuildServiceProvider();

            new RunTests().Start(serviceProvider);
        }
    }
}