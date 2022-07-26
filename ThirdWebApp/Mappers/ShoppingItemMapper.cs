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
            ShoppingListId = item.ShoppingListId,
            ShoppingListName = item.ShoppingList.Name,
        };
    }

    public static ShoppingItem ToModel(this CreateShoppingItemRequest request)
    {
        return new ShoppingItem
        {
            ItemName = request.ItemName,
            ShoppingListId = request.ShoppingListId
        };
    }

    public static ShoppingItemUpdate ToUpdateable(this ShoppingItem item)
    {
        return new ShoppingItemUpdate
        {
            ItemName = item.ItemName,
            IsPurchased = item.IsPurchased,
            ShoppingListId = item.ShoppingListId
        };
    }

    public static void MergeWithUpdatedProperties(this ShoppingItem item, ShoppingItemUpdate updated)
    {
        item.ItemName = updated.ItemName;
        item.IsPurchased = updated.IsPurchased;
        item.ShoppingListId = updated.ShoppingListId;
    }
}