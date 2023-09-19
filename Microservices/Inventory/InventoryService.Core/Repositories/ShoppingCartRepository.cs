using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Core.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IRepository<ShoppingCart> _context;

    public ShoppingCartRepository(IRepository<ShoppingCart> context)
    {
        _context = context;
    }

    public async Task<IQueryable<ShoppingCart>> Queryable(CancellationToken cancellationToken = default)
    {
        return await _context.Queryable(cancellationToken);
    }

    public async Task AddAsync(ShoppingCart item, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(item, cancellationToken);
    }

    public async Task UpdateAsync(ShoppingCart item, CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(item, cancellationToken);
    }

    public async Task DeleteAsync(ShoppingCart item, CancellationToken cancellationToken = default)
    {
        await _context.DeleteAsync(item, cancellationToken);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }


    public async Task<ShoppingCart?> GetShoppingCartByUsername(string username, CancellationToken token = default)
    {
        var result = await Queryable(token);
        return await result.FirstOrDefaultAsync(
            e => e.Username == username && e.IsDeleted == false && e.Status == ShoppingCart.CheckoutStatus.None, token);
    }

    public async Task<ShoppingCart?> GetShoppingCartById(Guid id, CancellationToken token = default)
    {
        var result = await Queryable(token);
        return await result.FirstOrDefaultAsync(
            e => e.Id == id && e.IsDeleted == false, token);
    }
}