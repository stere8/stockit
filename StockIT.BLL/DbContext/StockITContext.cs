using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace StockIT.Models;

public class StockITContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure connection string to your database here
        optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=oracle_stockit;User ID=oracle_stockit;Password=Oreste47@oracle;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;"
            );
    }
    public StockITContext(DbContextOptions<StockITContext> options) : base(options)
    {
    }
}
