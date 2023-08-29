using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Repositories;

public class ReviewRepository : IReviewRepository
{
    public async Task<bool> Create(Review item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(Review item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Review item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public HashSet<Review> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Review GetById(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public float GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}