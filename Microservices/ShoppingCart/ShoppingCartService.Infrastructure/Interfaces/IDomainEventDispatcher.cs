using ShoppingCartService.Core.Models;

namespace ShoppingCartService.Infrastructure.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
}