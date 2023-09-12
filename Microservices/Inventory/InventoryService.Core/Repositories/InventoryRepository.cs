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

    public Inventory Create(Inventory item, CancellationToken cancellationToken = default)
    {
        var result =  _context.Create(item, cancellationToken);
         SaveChanges();
        return result;
    }

    public  void Update(Inventory item, CancellationToken cancellationToken = default)
    {
         _context.Update(item, cancellationToken);
         SaveChanges();
    }

    public void Delete(Inventory item, CancellationToken cancellationToken = default)
    {
         _context.Delete(item, cancellationToken);
         SaveChanges();
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges();
    }

    public HashSet<Inventory> GetAllInventory(CancellationToken token = default)
    {
        var result = Queryable(token).ToHashSet();
        return result;
    }

    public Inventory? GetById(Guid id, CancellationToken token = default)
    {
        return Queryable(token).FirstOrDefault(e => e.Id == id);
    }

    public Inventory? GetByName(string name, CancellationToken token = default)
    {
        return Queryable(token).FirstOrDefault(e => e.Name == name);
    }
}