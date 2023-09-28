using MediatR;
using ShoppingCartService.Core.Dtos;

namespace ShoppingCartService.Core.Notifications;

public class ItemRemovedFromShoppingCartEvent : INotification
{
    public readonly ChangeInventoryQuantityDto _dto;

    public ItemRemovedFromShoppingCartEvent(ChangeInventoryQuantityDto dto)
    {
        _dto = dto;
    }
}