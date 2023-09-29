using InvShopRevOrchestrator.Core.Dtos;

namespace InvShopRevOrchestrator.Core.Interfaces;

public interface IReviewService
{
    Task<Guid> AddReview(ReviewDto dto,string username, CancellationToken token = default);
    
    Task UpdateReview(ReviewDto dto,string username, CancellationToken token = default);

    Task DeleteReview(Guid id, CancellationToken token = default);
    
    Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default);

    Task<HashSet<ReviewDto>> GetReviewsByUsername(string username, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewByInventoryIdAndUsername(Guid id, string username, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default);
}