using OrderService.Core.Dtos;
using OrderService.Core.Entities;

namespace OrderService.Core.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    public Task<HashSet<Order>> GetAllOrders(CancellationToken token = default);

    public Task<Order?> GetById(Guid id, CancellationToken token = default);
    
    public Task<Order?> GetByShoppingCartId(Guid id, CancellationToken token = default);
    
    public Task<HashSet<Order>> GetOrdersByUsername(string username, CancellationToken token = default);
}
