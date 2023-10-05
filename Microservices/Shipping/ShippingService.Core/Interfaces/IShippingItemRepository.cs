using ShippingService.Core.Entities;

namespace ShippingService.Core.Interfaces;

public interface IShippingItemRepository : IRepository<ShippingItem>
{
    public Task<HashSet<ShippingItem>> GetAllShippingItems(CancellationToken token = default);

    public Task<ShippingItem?> GetById(Guid id, CancellationToken token = default);
    
    public Task<ShippingItem?> GetByOrderId(Guid id, CancellationToken token = default);
}
    
