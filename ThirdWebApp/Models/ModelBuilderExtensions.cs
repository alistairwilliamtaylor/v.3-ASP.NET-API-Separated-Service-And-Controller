using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Models;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingList>()
            .HasData(
                new ShoppingList() { Id = 1, Name = "Fruit" },
                new ShoppingList() { Id = 2, Name = "Vegetables"});

        modelBuilder.Entity<ShoppingItem>()
            .HasData(
                new ShoppingItem()
                {
                    Id = 1,
                    ItemName = "Oranges",
                    IsPurchased = false,
                    ShoppingListId = 1
                },
                new ShoppingItem()
                {
                    Id = 2,
                    ItemName = "Lemons",
                    IsPurchased = false,
                    ShoppingListId = 1
                },
                new ShoppingItem
                {
                    Id = 3,
                    ItemName = "Carrots",
                    IsPurchased = false,
                    ShoppingListId = 2
                },
                new ShoppingItem
                {
                    Id = 4,
                    ItemName = "Potatoes",
                    IsPurchased = false,
                    ShoppingListId = 2
                }
            );
    }
}