using ReviewService.Core.Models;

namespace ReviewService.Core.Interfaces;

public interface IReviewRepository : IRepository<Review>
{
    public Task<HashSet<Review>> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token = default);

    public Task<Review?> GetById(Guid id, CancellationToken token = default);
    
    public Task<Review?> GetByInventoryIdAndUsername(Guid inventoryId, string username, CancellationToken token = default);
    
    public Task<HashSet<Review>> GetByUsername(string username, CancellationToken token = default);

    public Task GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token = default);
}
    
