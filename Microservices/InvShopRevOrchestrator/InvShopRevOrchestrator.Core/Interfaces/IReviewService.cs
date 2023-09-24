using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IReviewService
{
    Task<Guid> AddReview(ReviewDto dto, CancellationToken token = default);
    
    Task UpdateReview(ReviewDto dto, CancellationToken token = default);

    Task DeleteReview(ReviewDto dto, CancellationToken token = default);
    
    Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default);

    Task<HashSet<ReviewDto>> GetReviewsByUserId(int userId, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewByUserIdAndInventoryId(Guid id, int userId, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default);
}