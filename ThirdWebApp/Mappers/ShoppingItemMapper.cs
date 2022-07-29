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

    public static ShoppingItem ToModel(this ShoppingItemRequestBody request)
    {
        return new ShoppingItem
        {
            ItemName = request.ItemName,
            IsPurchased = request.IsPurchased,
            ShoppingListId = request.ShoppingListId
        };
    }

    public static ShoppingItemRequestBody ToUpdateable(this ShoppingItem item)
    {
        return new ShoppingItemRequestBody
        {
            ItemName = item.ItemName,
            IsPurchased = item.IsPurchased,
            ShoppingListId = item.ShoppingListId
        };
    }

    public static void MergeWithUpdatedProperties(this ShoppingItem item, ShoppingItemRequestBody updated)
    {
        item.ItemName = updated.ItemName;
        item.IsPurchased = updated.IsPurchased;
        item.ShoppingListId = updated.ShoppingListId;
    }
}