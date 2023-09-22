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
    
    public async Task<IQueryable<Inventory>> Queryable(CancellationToken cancellationToken = default)
    {
        return await _context.Queryable(cancellationToken);
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

    public async Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default)
    {
        var result = await Queryable(token);
        return result.Where(e=>e.IsDeleted == false).ToHashSet();
    }

    public async Task<Inventory?> GetById(Guid id, CancellationToken token = default)
    {
        var result = await Queryable(token);
        return await result.FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false, token);
    }

    public async Task<Inventory?> GetByName(string name, CancellationToken token = default)
    {
        var result = await Queryable(token);
        return await result.FirstOrDefaultAsync(e => e.Name == name && e.IsDeleted == false, token);
    }
}