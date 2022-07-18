using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models;

public class ShoppingListDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<ShoppingItemDto> Items { get; set; }
}