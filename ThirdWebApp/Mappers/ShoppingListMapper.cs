using FirstWebApp.Models;

namespace FirstWebApp.Mappers;

internal static class ShoppingListMapper
{
    public static ShoppingListDto ToDto(this ShoppingList list)
    {
        return new ShoppingListDto
        {
            Id = list.Id,
            Name = list.Name,
            Items = list.Items.Select(item => item.ToDto())
        };
    }
}