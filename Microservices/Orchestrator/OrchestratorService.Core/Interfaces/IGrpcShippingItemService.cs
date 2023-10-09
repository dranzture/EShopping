
using OrchestratorService.Core.Dtos.ShippingItem;

namespace OrchestratorService.Core.Interfaces;

public interface IGrpcShippingItemService
{
    Task UpdateShippingItemStatus(UpdateShippingStatusDto dto, CancellationToken token = default);
    
    Task<HashSet<ShippingItemDto>> GetAllShippingItems(CancellationToken token = default);
    
    Task<ShippingItemDto?> GetShippingItemById(Guid id, CancellationToken token = default);
    
    Task<ShippingItemDto?> GetShippingItemByOrderId(Guid orderId, CancellationToken token = default);
}