using ReviewService.Core.Dtos;
using ReviewService.Core.Models;

namespace ReviewService.Core.Interfaces;

public interface IReviewService
{
    Task<string> AddReview(ReviewDto dto, CancellationToken token = default);
    
    Task UpdateReview(ReviewDto dto,CancellationToken token = default);

    Task DeleteReview(ReviewDto dto, CancellationToken token = default);
    
    Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default);

    Task<HashSet<ReviewDto>> GetReviewsByUsername(string username, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewByInventoryIdAndUsername(Guid id, string username, CancellationToken token = default);
    
    Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default);
}