using OrderService.Core.Helpers;
using ShippingService.Core.Dto;

namespace ShippingService.Core.Notifications;

public class CreateShippingNotification : DomainEventBase
{
    public OrderDto _dto { get; }

    public CreateShippingNotification(OrderDto dto)
    {
        _dto = dto;
    }
}