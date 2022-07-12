namespace FirstWebApp.Models;

public class ShoppingList
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual List<ShoppingItem> Items { get; set; }
}