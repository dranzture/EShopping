using ShippingService.Core.Dto;

namespace ShippingService.Core.Interfaces;

public interface IShippingItemService
{
    Task UpdateStatus(ShippingItemDto dto,CancellationToken token = default);
    
    Task<HashSet<ShippingItemDto>> GetAllShippingItems(CancellationToken token = default);

    Task<HashSet<ShippingItemDto>> GetShippingItemsByUsername(string username, CancellationToken token = default);
    
    Task<ShippingItemDto?> GetShippingItemById(Guid id, CancellationToken token = default);
}