using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ReviewService.Core.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly IRepository<Review> _context;

    public ReviewRepository(IRepository<Review> context)
    {
        _context = context;
    }

    public IQueryable<Review> Queryable(CancellationToken cancellationToken = default)
    {
        return _context.Queryable(cancellationToken);
    }

    public async Task AddAsync(Review item, CancellationToken cancellationToken)
    {
        await _context.AddAsync(item, cancellationToken);
    }

    
    public async Task UpdateAsync(Review item, CancellationToken cancellationToken)
    {
        await _context.UpdateAsync(item, cancellationToken);
        
    }

    public async Task DeleteAsync(Review item, CancellationToken cancellationToken)
    {
        await _context.DeleteAsync(item, cancellationToken);
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChanges(cancellationToken);
    }


    public Task<HashSet<Review>> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token = default)
    {
        var result = Queryable(token).Where(e => e.InventoryId == inventoryId).ToHashSet();
        return Task.FromResult(result);
    }

    public async Task<Review?> GetById(Guid id, CancellationToken token = default)
    {
        return await Queryable(token).Where(e => e.Id == id).FirstOrDefaultAsync(token);
    }

    public async Task<Review?> GetByInventoryIdAndUsername(Guid inventoryId, string username,
        CancellationToken token = default)
    {
        var result = await Queryable(token).Where(e => e.InventoryId == inventoryId && e.Username == username)
            .FirstOrDefaultAsync(token);
        return result;
    }

    public  Task<HashSet<Review>> GetByUsername(string username, CancellationToken token = default)
    {
        var result = Queryable(token).Where(e =>e.Username == username)
            .ToHashSet();
        return Task.FromResult(result);
    }

    public async Task GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token = default)
    {
        var avg = await Queryable(token).Where(e => e.InventoryId == inventoryId)
            .AverageAsync(e => e.Stars, token);
        var count = await Queryable(token).Where(e => e.InventoryId == inventoryId)
            .CountAsync(token);
    }
}