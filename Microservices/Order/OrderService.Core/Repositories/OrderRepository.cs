using Microsoft.EntityFrameworkCore;
using OrderService.Core.Entities;
using OrderService.Core.Interfaces;

namespace OrderService.Core.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IRepository<Order> _context;

    public OrderRepository(IRepository<Order> context)
    {
        _context = context;
    }
    public async Task<IQueryable<Order>> Queryable(CancellationToken cancellationToken = default)
    {
        return await _context.Queryable(cancellationToken);
    }

    public async Task AddAsync(Order item, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(item, cancellationToken);
    }

    public async Task UpdateAsync(Order item, CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(item, cancellationToken);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<HashSet<Order>> GetAllOrders(CancellationToken token = default)
    {
        var result = await _context.Queryable(token);
        return result.AsNoTracking().ToHashSet();
    }

    public async Task<Order?> GetById(Guid id, CancellationToken token = default)
    {
        var result = await _context.Queryable(token);
        return await result.AsNoTracking().FirstOrDefaultAsync(e => e.Id==id, token);
    }
    
    public async Task<Order?> GetByShoppingCartId(Guid shoppingCartId, CancellationToken token = default)
    {
        var result = await _context.Queryable(token);
        return await result.AsNoTracking().FirstOrDefaultAsync(e => e.ShoppingCartId==shoppingCartId, token);
    }
    
    public async Task<HashSet<Order>> GetOrdersByUsername(string username, CancellationToken token = default)
    {
        var result = await _context.Queryable(token);
        return result.AsNoTracking().Where(e=>e.Username==username).ToHashSet();
    }
}