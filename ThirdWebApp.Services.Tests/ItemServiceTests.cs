using System;
using System.Collections.Generic;
using System.Linq;
using FirstWebApp.Exceptions;
using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace ThirdWebApp.Services.Tests;

public class ItemServiceTests : IDisposable
{
    private readonly ShoppingContext _context;
    private readonly ItemService _service;

    public ItemServiceTests()
    {
        var contextOptions = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase("Shopping Item Service Tests")
            .Options;

        _context = new ShoppingContext(contextOptions);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _service = new ItemService(_context);
    }

    public void Dispose() => _context.Dispose();

    public JsonPatchDocument<ShoppingItemRequestBody> CreateJsonPatchDocument(List<Operation<ShoppingItemRequestBody>> operations)
    {
        return new JsonPatchDocument<ShoppingItemRequestBody>(
            operations,
            new DefaultContractResolver()
        );
    }

    [Fact]
    public async void Gets_Item_With_Name_Of_Shopping_List_To_Which_It_Belongs()
    {
        // Act
        var retrievedItem = await _service.GetItem(1);

        // Assert
        Assert.Equal("Oranges", retrievedItem?.ItemName);
        Assert.Equal("Fruit", retrievedItem?.ShoppingList.Name);
    }

    [Fact]
    public async void Gets_All_Items_With_Names_Of_Respective_Shopping_Lists()
    {
        // Arrange
        var expectedItemNames = new[] {"Oranges", "Lemons", "Carrots", "Potatoes"};
        var expectedShoppingListNames = new[] {"Fruit", "Fruit", "Vegetables", "Vegetables"};

        // Act
        var retrievedItems = await _service.GetItems();
        var itemNames = retrievedItems.Select(item => item.ItemName);
        var shoppingListNames = retrievedItems.Select(item => item.ShoppingList.Name);

        // Assert
        Assert.Equal(expectedItemNames, itemNames);
        Assert.Equal(expectedShoppingListNames, shoppingListNames);
    }

    [Fact]
    public async void Adds_New_Item_To_Existent_Shopping_List()
    {
        // Arrange
        var itemToAdd = new ShoppingItem
        {
            Id = 0,
            ItemName = "Mangoes",
            IsPurchased = false,
            ShoppingListId = 1,
            ShoppingList = null
        };

        // Act
        var addedItem = await _service.AddItem(itemToAdd);

        // Assert
        Assert.Equal(5, addedItem.Id);
        Assert.Equal("Mangoes", addedItem.ItemName);
        Assert.Equal("Fruit", addedItem.ShoppingList.Name);
    }

    [Fact]
    public async void Cant_Add_Item_To_Shopping_List_Which_Doesnt_Exist()
    {
        // Arrange
        var itemBelongingToNonExistentShoppingList3 = new ShoppingItem
        {
            Id = 0,
            ItemName = "Walnuts",
            IsPurchased = false,
            ShoppingListId = 150,
        };

        // Act
        var addToNonExistentList = async () => await _service.AddItem(itemBelongingToNonExistentShoppingList3);

        // Assert
        await Assert.ThrowsAsync<ForeignKeyDoesNotExistException>(addToNonExistentList);
    }

    [Fact]
    public async void Delete_Item_Returns_The_Item_That_Has_Been_Deleted()
    {
        // Act
        var deletedItem = await _service.DeleteItem(1);
        
        // Assert
        Assert.Equal(1, deletedItem?.Id);
        Assert.Equal("Oranges", deletedItem?.ItemName);
        Assert.Equal(false, deletedItem?.IsPurchased);
        Assert.Equal(1, deletedItem?.ShoppingListId);
        Assert.Equal("Fruit", deletedItem?.ShoppingList.Name);
    }

    [Fact]
    public async void Delete_Item_Removes_The_Item_From_The_Database()
    {
        // Act
        await _service.DeleteItem(1);
        
        // Assert
        var itemAfterDeletion = await _service.GetItem(1);
        Assert.Null(itemAfterDeletion);
    }

    [Fact]
    public async void Replaces_Item_Properties_With_Specified_Updated_Values()
    {
        // Arrange
        var expectedUpdatedValues = new ShoppingItemRequestBody
        {
            ItemName = "Pumpkin",
            IsPurchased = false,
            ShoppingListId = 2
        };

        // Act
        await _service.ReplaceItemProperties(1, expectedUpdatedValues);

        // Assert
        var updatedItem = await _service.GetItem(1);
        Assert.Equal(expectedUpdatedValues.ItemName, updatedItem?.ItemName);
        Assert.Equal(expectedUpdatedValues.IsPurchased, updatedItem?.IsPurchased);
        Assert.Equal(expectedUpdatedValues.ShoppingListId, updatedItem?.ShoppingListId);
        Assert.Equal("Vegetables", updatedItem?.ShoppingList.Name);
    }

    [Fact]
    public async void Cant_Assign_To_Shopping_List_Which_Doesnt_Exist_When_Updating()
    {
        // Arrange
        var invalidUpdatedValues = new ShoppingItemRequestBody
        {
            ItemName = "Mangoes",
            IsPurchased = false,
            ShoppingListId = 150
        };
        
        // Act
        var updateToBelongToNonExistentList = async () => await _service.ReplaceItemProperties(1, invalidUpdatedValues);

        // Assert
        await Assert.ThrowsAsync<ForeignKeyDoesNotExistException>(updateToBelongToNonExistentList);
    }

    [Fact]
    public async void Updates_Item_Name_By_Applying_Patch_Document()
    {
        // Arrange
        var changeNameToKumquatOperations = new List<Operation<ShoppingItemRequestBody>>()
        {
            new () {
                path = "/ItemName",
                op = "add",
                value = "Kumquat"
            }   
        };
        var jsonPatchToChangeNameToKumquat = CreateJsonPatchDocument(changeNameToKumquatOperations);

        // Act
        var patchedItem = await _service.PatchItemProperties(1, jsonPatchToChangeNameToKumquat);

        // Assert
        Assert.Equal("Kumquat", patchedItem?.ItemName);
        Assert.Equal("Fruit", patchedItem?.ShoppingList.Name);
    }
    
    [Fact]
    public async void Cant_Patch_Item_To_Belong_To_Shopping_List_Which_Doesnt_Exist()
    {
        // Arrange
        var operationsToAssignItemToNonExistentShoppingList4 = new List<Operation<ShoppingItemRequestBody>>()
        {
            new () {
                path = "/ShoppingListId",
                op = "add",
                value = 4
            }   
        };
        var jasonPatchToAssignItemToNonExistentList4 = CreateJsonPatchDocument(operationsToAssignItemToNonExistentShoppingList4);
        
        // Act
        var applyPatchAssigningItemToNonExistentList =
            async () => await _service.PatchItemProperties(1, jasonPatchToAssignItemToNonExistentList4);

        // Assert
        await Assert.ThrowsAsync<ForeignKeyDoesNotExistException>(applyPatchAssigningItemToNonExistentList);
    }
    
}