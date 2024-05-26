using KhumaloCraft.Models;
using Microsoft.EntityFrameworkCore;


namespace KhumaloCraft.Data
{
    public class KhumaloCraftAppContext : DbContext
    {
        public KhumaloCraftAppContext(DbContextOptions<KhumaloCraftAppContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
      
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }

}
