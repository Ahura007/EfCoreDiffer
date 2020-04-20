using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EfPooling
{
    public static class SetupDatabase
    {
        public static void Run(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<BloggingContext>();
                var blogs = db.Blogs.IgnoreQueryFilters().ToList();

                if (db.Database.EnsureCreated())
                {
                    db.Blogs.Add(new Blog { Name = "The Dog Blog", Url = "http://sample.com/dogs" });
                    db.Blogs.Add(new Blog { Name = "The Cat Blog", Url = "http://sample.com/cats" });
                    db.SaveChanges();
                }
            }
        }
    }
}