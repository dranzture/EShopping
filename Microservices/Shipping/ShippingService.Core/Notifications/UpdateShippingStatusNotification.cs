using OrderService.Core.Helpers;
using ShippingService.Core.Dto;

namespace ShippingService.Core.Notifications;

public class UpdateShippingStatusNotification : DomainEventBase
{
    public ShippingItemDto _dto { get; }

    public UpdateShippingStatusNotification(ShippingItemDto dto)
    {
        _dto = dto;
    }
}