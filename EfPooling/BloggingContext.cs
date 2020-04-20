using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace EfPooling
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
            Interlocked.Increment(ref RunTests.ContextInstances);
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasQueryFilter(c => !c.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }
    }
}