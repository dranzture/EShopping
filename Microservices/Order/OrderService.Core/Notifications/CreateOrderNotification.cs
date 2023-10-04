using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Helpers;

namespace OrderService.Core.Notifications;

public class CreateOrderNotification : DomainEventBase
{
    public CreateOrderDto Dto { get; }

    public CreateOrderNotification(CreateOrderDto dto)
    {
        Dto = dto;
        EventOccurred = DateTimeOffset.Now;
    }
}