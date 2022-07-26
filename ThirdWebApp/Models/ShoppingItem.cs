using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FirstWebApp.Models;

public class ShoppingItem
{
    public int Id { get; set; }
    public string ItemName { get; set; } = String.Empty;
    public bool IsPurchased { get; set; }
    public int ShoppingListId { get; set; }
    public ShoppingList ShoppingList { get; set; }
}