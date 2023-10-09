using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Helpers;

namespace OrderService.Core.Notifications;

public class UpdateOrderStatusNotification : DomainEventBase
{
    public OrderDto Dto { get; }

    public UpdateOrderStatusNotification(OrderDto dto)
    {
        Dto = dto;
        EventOccurred = DateTimeOffset.Now;
    }
}