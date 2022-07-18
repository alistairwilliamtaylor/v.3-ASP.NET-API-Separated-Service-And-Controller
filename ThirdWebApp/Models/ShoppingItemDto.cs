using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models;

public class ShoppingItemDto
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public bool IsPurchased { get; set; }
    public int ShoppingListId { get; set; }
    public string ShoppingListName { get; set; }
}