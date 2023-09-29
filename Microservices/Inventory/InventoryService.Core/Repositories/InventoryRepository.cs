using InventoryService.Core.Entities;
using InventoryService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Core.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly IRepository<Inventory> _context;

    public InventoryRepository(IRepository<Inventory> context)
    {
        _context = context;
    }

    public IQueryable<Inventory> Queryable(CancellationToken cancellationToken = default)
    {
        return _context.Queryable(cancellationToken);
    }

    public async Task AddAsync(Inventory item, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(item, cancellationToken);
    }

    public async Task UpdateAsync(Inventory item, CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(item, cancellationToken);
    }

    public async Task DeleteAsync(Inventory item, CancellationToken cancellationToken = default)
    {
        await _context.DeleteAsync(item, cancellationToken);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default)
    {
        var result = Queryable(token)
            .Where(e => e.IsDeleted == false)
            .ToHashSet();
        
        return Task.FromResult(result);
    }

    public async Task<Inventory?> GetById(Guid id, CancellationToken token = default)
    {
        var result = await Queryable(token)
            .Where(e => e.Id == id && e.IsDeleted == false)
            .FirstOrDefaultAsync(token);

        return result;
    }

    public async Task<Inventory?> GetByName(string name, CancellationToken token = default)
    {
        var result = await Queryable(token)
            .Where(e => e.Name == name && e.IsDeleted == false)
            .FirstOrDefaultAsync(token);

        return result;
    }
}