using FirstWebApp.Models;
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
        return _context.Items.ToArrayAsync();
    }

    public ValueTask<ShoppingItem?> GetItem(int id)
    {
        return _context.Items.FindAsync(id);
    }

    public Task<int> CreateItem(ShoppingItem item)
    {
        _context.Items.Add(item);
        return _context.SaveChangesAsync();
    }

    public async Task<ShoppingItem?> DeleteItem(int id)
    {
        var item = await GetItem(id);
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