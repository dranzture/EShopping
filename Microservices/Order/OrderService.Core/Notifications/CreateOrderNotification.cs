using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Helpers;

namespace OrderService.Core.Notifications;

public class CreateOrderNotification : DomainEventBase
{
    public OrderDto Dto { get; }

    public CreateOrderNotification(OrderDto dto)
    {
        Dto = dto;
        EventOccurred = DateTimeOffset.Now;
    }
}