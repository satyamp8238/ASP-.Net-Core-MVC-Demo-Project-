using Microsoft.EntityFrameworkCore;

namespace DemoProject.Models
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }  

        public DbSet<Product> Products { get; set;   }

        protected override void OnModelCreating(ModelBuilder foModelBuilder)
        {
            base.OnModelCreating(foModelBuilder);
            foModelBuilder.Entity<Product>().HasKey(x => new { x.inId });
        }

    }
}
