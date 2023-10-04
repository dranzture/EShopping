using MediatR;

namespace OrderService.Core.Helpers;

public abstract class DomainEventBase : INotification
{
    public DateTimeOffset EventOccurred { get; protected set; } = DateTimeOffset.Now;
}