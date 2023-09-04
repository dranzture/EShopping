using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IReviewRepository : IRepository<Review>
{
    public Task<HashSet<Review>> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token = default);

    public Task<Review?> GetById(Guid id, CancellationToken token = default);
    
    public Task<Review?> GetByUserIdAndInventoryId(Guid inventoryId, int userId,CancellationToken token = default);
    
    public Task<HashSet<Review>> GetByUserId(int userId, CancellationToken token = default);

    public Task GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token = default);
}
    
