using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace StockIT.Models;

public class StockITContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure connection string to your database here
        optionsBuilder.UseSqlServer("Server=SILVERBACK\\SQLEXPRESS;Database=StockIT;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
    }
    public StockITContext(DbContextOptions<StockITContext> options) : base(options)
    {
    }
}
