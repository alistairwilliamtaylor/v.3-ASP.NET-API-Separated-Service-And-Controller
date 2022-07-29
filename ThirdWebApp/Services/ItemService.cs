using FirstWebApp.Exceptions;
using FirstWebApp.Mappers;
using FirstWebApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Services;

public class ItemService
{
    private readonly ShoppingContext _context;

    public ItemService(ShoppingContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public Task<ShoppingItem[]> GetItems()
    {
        return _context.Items
            .Include(item => item.ShoppingList)
            .ToArrayAsync();
    }

    public Task<ShoppingItem?> GetItem(int id) 
    {
        return _context.Items
            .Include(item => item.ShoppingList)
            .SingleOrDefaultAsync(item => item.Id == id);
    }

    public async Task<ShoppingItem> AddItem(ShoppingItem newItem)
    {
        await ValidateForeignKey(newItem);
        _context.Items.Add(newItem);
        await _context.SaveChangesAsync();
        var createdItemWithNavigationProperty = await GetItem(newItem.Id);
        return createdItemWithNavigationProperty!;
    }

    public async Task<ShoppingItem?> DeleteItem(int id)
    {
        var item = await GetItem(id);
        if (item == null) return null;
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task ReplaceItemProperties(int id, ShoppingItemRequestBody updatedFields)
    {
        var itemModel = await GetItem(id);
        // get the patch shopping ListId and if it doesnt exist throw an exception
        itemModel.MergeWithUpdatedProperties(updatedFields);
        _context.Entry(itemModel).State = EntityState.Modified;
        await ValidateForeignKey(itemModel);
        await _context.SaveChangesAsync();
    }
    
    public async Task<ShoppingItem?> PatchItemProperties(int id, JsonPatchDocument<ShoppingItemRequestBody> patch)
    {
        var itemModel = await GetItem(id);
        if (itemModel == null) return null;
        var patchableProperties = itemModel.ToUpdateable();
        patch.ApplyTo(patchableProperties);
        itemModel.MergeWithUpdatedProperties(patchableProperties);
        _context.Entry(itemModel).State = EntityState.Modified;
        await ValidateForeignKey(itemModel);
        await _context.SaveChangesAsync();
        var updatedItemWithNavigationProperty = await GetItem(itemModel.Id);
        return updatedItemWithNavigationProperty!;
    }

    public async Task ValidateForeignKey(ShoppingItem item)
    {
        var hasValidForeignKey = await _context.Lists.AnyAsync(list => list.Id == item.ShoppingListId); 
        if (!hasValidForeignKey) throw new ForeignKeyDoesNotExistException($"There is no list with id {item.ShoppingListId}");
    }
}