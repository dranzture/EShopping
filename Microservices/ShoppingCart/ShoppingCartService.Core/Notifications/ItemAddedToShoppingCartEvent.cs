using MediatR;
using ShoppingCartService.Core.Dtos;

namespace ShoppingCartService.Core.Notifications;

public class ItemAddedToShoppingCartEvent : INotification
{
    public readonly ChangeInventoryQuantityDto _dto;

    public ItemAddedToShoppingCartEvent(ChangeInventoryQuantityDto dto)
    {
        _dto = dto;
    }
}