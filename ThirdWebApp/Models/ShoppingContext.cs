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
    
    public DbSet<ShoppingItem> Items { get; set; }
    public DbSet<ShoppingList> Lists { get; set; }
}

