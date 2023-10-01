using ShoppingCartService.Core.Entities;

namespace ShoppingCartService.Infrastructure.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
}