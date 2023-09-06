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

    public async Task Create(Review item, CancellationToken cancellationToken)
    {
        await _context.Create(item, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task Update(Review item, CancellationToken cancellationToken)
    {
        await _context.Update(item, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task Delete(Review item, CancellationToken cancellationToken)
    {
        await _context.Delete(item, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChanges(cancellationToken);
    }


    public async Task<HashSet<Review>> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token = default)
    {
        var result = await Task.Run(() => Queryable(token).Where(e => e.InventoryId == inventoryId).ToHashSet(), token);
        return result;
    }

    public async Task<Review> GetById(Guid id, CancellationToken token = default)
    {
        return await Queryable(token).Where(e => e.Id == id).FirstAsync(token);
    }

    public async Task<Review?> GetByUserIdAndInventoryId(Guid inventoryId, int userId,
        CancellationToken token = default)
    {
        var result = await Queryable(token).Where(e => e.InventoryId == inventoryId && e.ExternalUserId == userId)
            .FirstOrDefaultAsync(token);
        return result;
    }

    public async Task<HashSet<Review>> GetByUserId(int userId, CancellationToken token = default)
    {
        var result =
            await Task.Run(
                () => Queryable(token).Where(e => e.ExternalUserId == userId)
                    .ToHashSet(), token);
        return result;
    }

    public async Task GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token = default)
    {
        var avg = await Queryable(token).Where(e => e.InventoryId == inventoryId)
            .AverageAsync(e => e.Stars, token);
        var count = await Queryable(token).Where(e => e.InventoryId == inventoryId)
            .CountAsync(token);
    }
}