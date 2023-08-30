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


    public async Task<HashSet<Review>> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Review> GetById(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Review> GetByUsernameAndInventoryId(Guid inventoryId, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Review> GetByUsername(string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
    
    public async Task GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}