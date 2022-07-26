using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace ThirdWebApp.Services.Tests;

public class ItemServiceTests
{
    private readonly DbContextOptions<ShoppingContext> _contextOptions;

    private readonly ShoppingList _fruitList = new() {Id = 1, Name = "Fruit"};
    
    private readonly ShoppingItem _orangesItem = new ()
    {
        Id = 1,
        ItemName = "Oranges",
        IsPurchased = false,
        ShoppingListId = 1
    };
    
    public ItemServiceTests()
    {
        _contextOptions = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase("ShoppingItemControllerTest")
            // .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new ShoppingContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.AddRange(_fruitList, _orangesItem);

        context.SaveChanges();
    }
    
    ShoppingContext CreateContext() => new ShoppingContext(_contextOptions, (context, modelBuilder) =>
    {
        modelBuilder.Entity<UrlResource>()
            .ToInMemoryQuery(() => context.Blogs.Select(b => new UrlResource { Url = b.Url }));
    });
    

    [Fact]
    public async void Gets_Item_By_Id()
    {
        await using var context = CreateContext();
        var service = new ItemService(context);

        var retrievedItem = await service.GetItem(1);

        Assert.Equal("Oranges", retrievedItem.ItemName);
        Assert.Equal("Fruit", retrievedItem.ShoppingList.Name);
    }
    
}