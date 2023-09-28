using MediatR;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Notifications;

namespace ShoppingCartService.Core.Handlers;

public class ItemAddedToShoppingCartHandler : INotificationHandler<ItemAddedToShoppingCartEvent>
{
    private readonly IPublisher<ChangeInventoryQuantityDto> _publisher;

    public ItemAddedToShoppingCartHandler(IPublisher<ChangeInventoryQuantityDto> publisher)
    {
        _publisher = publisher;
    }
    public async Task Handle(ItemAddedToShoppingCartEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.ProcessMessage(IPublisher<ChangeInventoryQuantityDto>.DecreaseInventoryTopic, Guid.NewGuid().ToString(),
            new ChangeInventoryQuantityDto() { InventoryId = notification._dto.InventoryId, Quantity = notification._dto.Quantity });
    }

}