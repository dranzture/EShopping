using MediatR;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Notifications;

namespace ShoppingCartService.Core.Handlers;

public class ItemRemovedFromShoppingCartHandler : INotificationHandler<ItemRemovedFromShoppingCartEvent>
{
    private readonly IPublisher<ChangeInventoryQuantityDto> _publisher;

    public ItemRemovedFromShoppingCartHandler(IPublisher<ChangeInventoryQuantityDto> publisher)
    {
        _publisher = publisher;
    }
    public async Task Handle(ItemRemovedFromShoppingCartEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.ProcessMessage(IPublisher<ChangeInventoryQuantityDto>.IncreaseInventoryTopic, Guid.NewGuid().ToString(),
            new ChangeInventoryQuantityDto() { InventoryId = notification._dto.InventoryId, Quantity = notification._dto.Quantity });
    }
}