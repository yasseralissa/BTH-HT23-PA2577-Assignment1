using BTH.H23.PA2577.Assignment1.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace BTH.H23.PA2577.Assignment1.ProductService
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}