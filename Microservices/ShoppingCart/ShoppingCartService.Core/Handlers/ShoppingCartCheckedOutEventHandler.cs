using MediatR;
using ShoppingCartService.Core.Interfaces;
using ShoppingCartService.Core.Notifications;

namespace ShoppingCartService.Core.Handlers;

public class ShoppingCartCheckedOutEventHandler : INotificationHandler<ShoppingCartCheckedOutEvent>
{
    private readonly IShoppingCartService _service;

    public ShoppingCartCheckedOutEventHandler(IShoppingCartService service)
    {
        _service = service;
    }
    
    public async Task Handle(ShoppingCartCheckedOutEvent notification, CancellationToken cancellationToken)
    {
        await _service.AddShoppingCart(notification.Username, cancellationToken);
    }
}