using OrderService.Core.Helpers;
using ShippingService.Core.Dto;

namespace ShippingService.Core.Notifications;

public class CreateShippingNotification : DomainEventBase
{
    public ShippingItemDto _dto { get; }

    public CreateShippingNotification(ShippingItemDto dto)
    {
        _dto = dto;
    }
}