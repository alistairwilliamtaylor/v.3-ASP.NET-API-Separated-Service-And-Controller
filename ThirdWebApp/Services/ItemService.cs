using FirstWebApp.Mappers;
using FirstWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FirstWebApp.Services;

public class ItemService
{
    private readonly ShoppingContext _context;

    public ItemService(ShoppingContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public Task<ShoppingItemDto[]> GetItems()
    {
        return _context.Items.
            Select(item => item.ToDto())
            .ToArrayAsync();
    }

    public Task<ShoppingItemDto?> GetItemDto(int id)
    {
        return _context.Items
            .Where(item => item.Id == id)
            .Select(item => item.ToDto())
            .FirstOrDefaultAsync();
    }

    private ValueTask<ShoppingItem?> GetItemModel(int id)
    {
        return _context.Items.FindAsync(id);
    }

    public async Task<int> CreateItem(CreateShoppingItemRequest itemRequest)
    {
        var newItem = itemRequest.ToModel();
        _context.Items.Add(newItem);
        await _context.SaveChangesAsync();
        return newItem.Id;
    }

    public async Task<ShoppingItem?> DeleteItem(int id)
    {
        var item = await GetItemModel(id);
        if (item == null) return null;
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public Task<int> UpdateItem(ShoppingItem item)
    {
        _context.Entry(item).State = EntityState.Modified;
        return _context.SaveChangesAsync();
    }
}