namespace FirstWebApp;

internal class InMemoryItemRepository : IItemRepository
{
    private Dictionary<int, ShoppingItem> _repo = new Dictionary<int, ShoppingItem>()
    {
        {1, new ShoppingItem
            {
                Id = 1,
                ItemName = "Milk",
                IsPurchased = false
            }
        },
        {2, new ShoppingItem
            {
                Id = 2,
                ItemName = "Eggs",
                IsPurchased = false
            }
        }
    };
    private int _nextId = 3;

    public ShoppingItem Add(ShoppingItem item)
    {
        item.Id = _nextId;
        _repo.Add(item.Id, item);
        _nextId++;
        return item;
    }
    
    public ShoppingItem GetById(int id) => _repo[id];

    public ShoppingItem Remove(int id)
    {
        var deletedItem = GetById(id);
        _repo.Remove(id);
        return deletedItem;
    }

    public ShoppingItem Replace(ShoppingItem updatedItem)
    {
        _repo[updatedItem.Id] = updatedItem;
        return updatedItem;
    }

    public IQueryable<ShoppingItem> Query() => _repo.Values.AsQueryable();


}