using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IReviewRepository : IRepositoryBase<Review>
{
    public HashSet<Review> GetAllReviewsByInventoryId(Guid inventoryId, CancellationToken token);

    public Review GetById(Guid id, CancellationToken token);

    public float GetStatisticsByInventoryId(Guid inventoryId, CancellationToken token);
}
    
