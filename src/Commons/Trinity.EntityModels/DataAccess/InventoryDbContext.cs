using Microsoft.EntityFrameworkCore;
using Trinity.EntityModels;
using Trinity.EntityModels.Models;

namespace Trinity.EntityModels.DataAccess
{
    public class InventoryDbContext : TrinityDbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Origin> Origins { get; set; }

        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
