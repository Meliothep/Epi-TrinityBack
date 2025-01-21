using Microsoft.EntityFrameworkCore;
using Trinity.EntityModels;
using Trinity.EntityModels.Models;

namespace Trinity.EntityModels.DataAccess
{
    public class CustomerDbContext : TrinityDbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options: options)
        { 
        }
    }
}
