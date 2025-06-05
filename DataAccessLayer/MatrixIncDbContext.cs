using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using KE03_INTDEV_SE_2_Base.Models;


namespace DataAccessLayer
{
    public class MatrixIncDbContext : DbContext
    {
        public MatrixIncDbContext(DbContextOptions<MatrixIncDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
               .HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerId)
               .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
