using System.Text.Json.Serialization;

namespace FirstWebApp.Models;

public class ShoppingList
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation Property
    public virtual IEnumerable<ShoppingItem> Items { get; set; }
}