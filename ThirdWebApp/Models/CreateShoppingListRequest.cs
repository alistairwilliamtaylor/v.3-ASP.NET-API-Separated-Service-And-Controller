using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models;

public class CreateShoppingListRequest
{
    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "List name must be between 1 and 30 characters")]
    public string Name { get; set; }
}