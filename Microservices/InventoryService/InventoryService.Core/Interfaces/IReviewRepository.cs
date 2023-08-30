using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IReviewRepository : IRepositoryBase<Review>
{
    public Task<HashSet<Review>> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token = default);

    public Task<Review> GetById(Guid id, CancellationToken token = default);
    
    public Task<Review> GetByUsernameAndInventoryId(Guid inventoryId, string username, CancellationToken token = default);
    
    public Task<Review> GetByUsername(string username, CancellationToken token = default);

    public Task GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token = default);
}
    
