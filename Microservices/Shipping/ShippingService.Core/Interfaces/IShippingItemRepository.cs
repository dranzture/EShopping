using ShippingService.Core.Entities;

namespace ShippingService.Core.Interfaces;

public interface IShippingItemRepository : IRepository<ShippingItem>
{
    public Task<HashSet<ShippingItem>> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token = default);

    public Task<ShippingItem?> GetById(Guid id, CancellationToken token = default);
    
    public Task<ShippingItem?> GetByInventoryIdAndUsername(Guid inventoryId, string username, CancellationToken token = default);
    
    public Task<HashSet<ShippingItem>> GetByUsername(string username, CancellationToken token = default);

    public Task GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token = default);
}
    
