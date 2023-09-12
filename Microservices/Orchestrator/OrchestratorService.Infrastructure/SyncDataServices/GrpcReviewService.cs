using OrchestratorService.Core.Dtos.Review;
using OrchestratorService.Core.Interfaces;

namespace OrchestratorService.Infrastructure.SyncDataServices;

public class GrpcReviewService : IGrpcReviewService
{
    public Task AddReview(ReviewDto request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateReview(ReviewDto request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteReview(ReviewDto request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewDto>> GetReviewsByInventoryId(UserAndInventoryIdParam request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewDto>> GetReviewByUserId(int userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewDto> GetReviewByUserIdAndInventoryId(UserAndInventoryIdParam request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}