using OrchestratorService.Core.Dtos.Review;

namespace OrchestratorService.Core.Interfaces;

public interface IGrpcReviewService
{
    Task AddReview(ReviewDto request, CancellationToken cancellationToken = default);
    Task UpdateReview(ReviewDto request, CancellationToken cancellationToken = default);
    Task DeleteReview(ReviewDto request, CancellationToken cancellationToken = default);
    Task<List<ReviewDto>> GetReviewsByInventoryId(UserAndInventoryIdParam request, CancellationToken cancellationToken = default);
    Task<List<ReviewDto>> GetReviewByUserId(int userId, CancellationToken cancellationToken = default);
    Task<ReviewDto> GetReviewByUserIdAndInventoryId(UserAndInventoryIdParam request, CancellationToken cancellationToken = default);
}