using BTH.H23.PA2577.Assignment1.OrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace BTH.H23.PA2577.Assignment1.OrderService
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderLines)
                .WithOne()
                .HasForeignKey(e => e.OrderId)
                .IsRequired();

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}
