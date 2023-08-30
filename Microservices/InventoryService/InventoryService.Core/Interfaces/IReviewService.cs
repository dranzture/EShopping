using InventoryService.Core.Dtos;
using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;

public interface IReviewService
{
    Task AddReview(ReviewDto dto, string username, CancellationToken token = default);
    
    Task UpdateReview(ReviewDto dto, string username, CancellationToken token = default);

    Task DeleteReview(ReviewDto dto, string username, CancellationToken token = default);
    
    Task IncreaseInventory(ReviewDto dto, string username, CancellationToken token = default);

    Task<HashSet<Review>> GetReviewsByInventoryId(ReviewDto dto, CancellationToken token = default);

    Task<HashSet<Review>> GetReviewByUsername(string username, CancellationToken token = default);
    
    Task<HashSet<Review>> GetReviewByUsernameAndInventoryId(Guid id, string username, CancellationToken token = default);
}