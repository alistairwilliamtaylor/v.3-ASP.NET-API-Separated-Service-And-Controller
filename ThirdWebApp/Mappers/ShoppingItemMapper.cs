using FirstWebApp.Models;

namespace FirstWebApp.Mappers;

internal static class ShoppingItemMapper
{
    public static ShoppingItemDto ToDto(this ShoppingItem item)
    {
        return new ShoppingItemDto()
        {
            Id = item.Id,
            ItemName = item.ItemName,
            IsPurchased = item.IsPurchased,
            ShoppingListId = item.Id,
            ShoppingListName = item.ShoppingList.Name,
        };
    }

    public static ShoppingItem ToModel(this CreateShoppingItemRequest request)
    {
        return new ShoppingItem
        {
            ItemName = request.ItemName,
            ShoppingListId = request.ShoppingListId,
        };
    }
}