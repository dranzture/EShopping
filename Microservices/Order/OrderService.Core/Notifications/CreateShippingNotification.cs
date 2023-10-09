using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Helpers;

namespace OrderService.Core.Notifications;

public class CreateShippingNotification : DomainEventBase
{
    public ShippingDto _dto { get; }

    public CreateShippingNotification(ShippingDto dto)
    {
        _dto = dto;
        EventOccurred = DateTimeOffset.Now;
    }
}