using OrderService.Core.Dtos;

namespace OrderService.Core.Interfaces;

public interface IOrderService
{
    Task ReprocessOrderById(Guid id,CancellationToken token = default);
    
    Task<HashSet<OrderDto>> GetAllOrders(CancellationToken token = default);

    Task<HashSet<OrderDto>> GetOrdersByUsername(string username, CancellationToken token = default);
    
    Task<OrderDto?> GetOrderById(Guid id, CancellationToken token = default);
}