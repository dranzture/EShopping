using ReviewService.Core.Dtos;
using ReviewService.Core.Models;

namespace ReviewService.Core.Interfaces;

public interface IReviewService
{
    Task<string> AddReview(ReviewDto dto, CancellationToken token = default);
    
    Task UpdateReview(ReviewDto dto, CancellationToken token = default);

    Task DeleteReview(ReviewDto dto, CancellationToken token = default);
    
    Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default);

    Task<HashSet<ReviewDto>> GetReviewsByUserId(int userId, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewByInventoryIdAndUserId(Guid id, int userId, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default);
}