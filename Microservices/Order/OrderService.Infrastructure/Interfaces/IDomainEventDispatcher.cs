using OrderService.Core.Entities;

namespace OrderService.Infrastructure.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
}