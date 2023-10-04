using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Helpers;

namespace OrderService.Core.Notifications;

public class CreateShippingNotification : DomainEventBase
{
    public OrderDto _dto { get; }

    public CreateShippingNotification(OrderDto dto)
    {
        _dto = dto;
        EventOccurred = DateTimeOffset.Now;
    }
}