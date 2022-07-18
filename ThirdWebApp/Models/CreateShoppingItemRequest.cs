using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models;

public class CreateShoppingItemRequest
{
    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "Item name must be between 1 and 30 characters")]
    public string ItemName { get; set; }
    [Required]
    public int ShoppingListId { get; set; }
}