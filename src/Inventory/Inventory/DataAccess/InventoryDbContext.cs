using Microsoft.EntityFrameworkCore;

namespace Inventory.DataAccess
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        { }

        //public DbSet<TeamUser> TeamUsers { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamUser>().HasKey(tu => new { tu.TeamId, tu.UserName });
            modelBuilder.Entity<TeamUser>().HasOne<Team>().WithMany().HasForeignKey(tu => tu.TeamId);
        }
        */
    }
}
