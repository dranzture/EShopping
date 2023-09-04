using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
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

    public async Task Create(Inventory item, CancellationToken cancellationToken = default)
    {
        await _context.Create(item, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task Update(Inventory item, CancellationToken cancellationToken = default)
    {
        await _context.Update(item, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task Delete(Inventory item, CancellationToken cancellationToken = default)
    {
        await _context.Delete(item, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChanges(cancellationToken);
    }

    public async Task<HashSet<Inventory>> GetAllInventory(CancellationToken token = default)
    {
        var result = await Task.Run(() => Queryable(token).ToHashSet(), token);
        return result;
    }

    public async Task<Inventory> GetById(Guid id, CancellationToken token = default)
    {
        return await Queryable(token).Where(e => e.Id == id).FirstAsync(token);
    }

    public async Task<Inventory> GetByName(string name, CancellationToken token = default)
    {
        return await Queryable(token).Where(e => e.Name == name).FirstAsync(token);
    }
}