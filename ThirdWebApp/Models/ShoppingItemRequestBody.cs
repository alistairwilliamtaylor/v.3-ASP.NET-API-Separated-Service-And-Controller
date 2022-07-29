using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models;

public class ShoppingItemRequestBody
{
    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "Item name must be between 1 and 30 characters")]
    public string ItemName { get; set; }
    [Required]
    public bool IsPurchased { get; set; }
    [Required]
    public int ShoppingListId { get; set; }
}