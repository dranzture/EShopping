using MediatR;
using OrderService.Core.Dtos;
using OrderService.Core.Helpers;

namespace OrderService.Core.Notifications;

public class UpdateOrderStatusNotification : DomainEventBase
{
    public UpdateOrderStatusDto Dto { get; }

    public UpdateOrderStatusNotification(UpdateOrderStatusDto dto)
    {
        Dto = dto;
        EventOccurred = DateTimeOffset.Now;
    }
}