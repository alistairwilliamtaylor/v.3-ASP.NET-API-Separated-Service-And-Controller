namespace FirstWebApp;

public interface IItemRepository
{
    ShoppingItem Add(ShoppingItem item);

    ShoppingItem GetById(int id);

    ShoppingItem Remove(int id);

    ShoppingItem Replace(ShoppingItem item);
    IQueryable<ShoppingItem> Query();
    
    
}