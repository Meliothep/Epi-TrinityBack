using Microsoft.EntityFrameworkCore;
using Trinity.EntityModels;
using Trinity.EntityModels.Models;

namespace Trinity.EntityModels.DataAccess
{
    public class CartDbContext : TrinityDbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses {get; set;}

        public DbSet<Category> Categories {get; set;}
        public DbSet<Brand> Brands {get; set;}
        public DbSet<Product> Products {get; set;}

        public DbSet<CartItem> CartItems {get; set;}
        public DbSet<Cart> Carts {get; set;}


        public CartDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}