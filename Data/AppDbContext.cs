using Microsoft.EntityFrameworkCore;
using TaskPronia.Models;

namespace TaskPronia.Data
{
    public class AppDbContext:DbContext 
    {
         public  AppDbContext (DbContextOptions<AppDbContext>  options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
