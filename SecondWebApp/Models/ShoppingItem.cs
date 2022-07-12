using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FirstWebApp.Models;

public class ShoppingItem
{
    public int Id { get; set; }
    [Required]
    public string ItemName { get; set; } = String.Empty;
    public bool IsPurchased { get; set; }

    [Required]
    public int ShoppingListId { get; set; }
    
    [JsonIgnore]
    public virtual ShoppingList? ShoppingList { get; set; }
}