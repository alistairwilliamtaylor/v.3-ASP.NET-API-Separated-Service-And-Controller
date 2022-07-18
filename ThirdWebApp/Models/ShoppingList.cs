namespace FirstWebApp.Models;

public class ShoppingList
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual IEnumerable<ShoppingItem> Items { get; set; }
}