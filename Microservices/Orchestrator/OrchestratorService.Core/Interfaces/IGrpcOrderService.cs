
using OrchestratorService.Core.Dtos.Order;

namespace OrchestratorService.Core.Interfaces;

public interface IGrpcOrderService
{
    Task ReprocessOrderById(Guid id, CancellationToken token = default);
    Task<HashSet<OrderDto>> GetAllOrders(CancellationToken token=default);
    Task<HashSet<OrderDto>> GetOrdersByUsername(string username, CancellationToken token = default);
    Task<OrderDto?> GetByOrderId(Guid orderId, CancellationToken token = default); 
}