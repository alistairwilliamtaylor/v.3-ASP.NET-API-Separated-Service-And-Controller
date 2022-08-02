using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Models;

public class ShoppingContext : DbContext
{
    public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=/Users/Alistair.Taylor/Documents/SQLiteDbs/ShoppingLists.db");
    }

    public DbSet<ShoppingItem> Items { get; set; }
    public DbSet<ShoppingList> Lists { get; set; }
}

