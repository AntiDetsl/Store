using Microsoft.EntityFrameworkCore;
using Store.Entities;

namespace Store.DAL.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderItem> Items { get; set; } = null!;

        public DbSet<Provider> Providers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(o => o.Number).HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Order>().Property(o => o.Date).HasColumnType("datetime2");
            modelBuilder.Entity<Order>().Property(o => o.Date).HasMaxLength(7);

            modelBuilder.Entity<OrderItem>().Property(o => o.Name).HasColumnType("nvarchar(max)");
            modelBuilder.Entity<OrderItem>().Property(o => o.Unit).HasColumnType("nvarchar(max)");

            modelBuilder.Entity<OrderItem>().Property(o => o.Quantity).HasPrecision(18, 3);

            modelBuilder.Entity<Provider>().Property(p => p.Name).HasColumnType("nvarchar(max)");
        }
    }
}
