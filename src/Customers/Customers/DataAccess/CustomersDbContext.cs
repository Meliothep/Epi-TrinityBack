using Microsoft.EntityFrameworkCore;

namespace Customers.DataAccess
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
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
