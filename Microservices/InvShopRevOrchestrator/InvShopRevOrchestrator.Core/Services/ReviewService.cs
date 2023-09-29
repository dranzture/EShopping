using InvShopRevOrchestrator.Core.Dtos;
using InvShopRevOrchestrator.Core.Interfaces;

namespace InvShopRevOrchestrator.Core.Services;

public class ReviewService : IReviewService
{
    private readonly IGrpcReviewService _grpcReviewService;
    private readonly IGrpcInventoryService _grpcInventoryService;

    public ReviewService(IGrpcReviewService grpcReviewService, IGrpcInventoryService grpcInventoryService)
    {
        _grpcReviewService = grpcReviewService;
        _grpcInventoryService = grpcInventoryService;
    }

    public async Task<Guid> AddReview(ReviewDto dto, string username, CancellationToken token = default)
    {
        var inventory = await _grpcInventoryService.GetById(dto.InventoryId, token);
        if (inventory == null)
        {
            throw new ArgumentException("Inventory not found");
        }

        var reviewId = await _grpcReviewService.AddReview(dto, username, token);
        return reviewId;
    }

    public async Task UpdateReview(ReviewDto dto, string username, CancellationToken token = default)
    {
        var review = await _grpcReviewService.GetReviewById(dto.Id, token);
        if (review == null)
        {
            throw new ArgumentException("Review not found");
        }

        await _grpcReviewService.UpdateReview(dto, username, token);
    }

    public async Task DeleteReview(Guid id, CancellationToken token = default)
    {
        var review = await _grpcReviewService.GetReviewById(id, token);
        if (review == null)
        {
            throw new ArgumentException("Review not found");
        }

        await _grpcReviewService.DeleteReview(review, token);
    }

    public async Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default)
    {
        var reviews = await _grpcReviewService.GetReviewsByInventoryId(id, token);
        return reviews;
    }

    public async Task<HashSet<ReviewDto>> GetReviewsByUsername(string username, CancellationToken token = default)
    {
        var reviews = await _grpcReviewService.GetReviewsByUsername(username, token);
        return reviews;
    }

 
    public async Task<ReviewDto?> GetReviewByInventoryIdAndUsername(Guid id, string username,
        CancellationToken token = default)
    {
        var review = await _grpcReviewService.GetReviewByInventoryIdAndUsername(id, username, token);
        return review;
    }

    public async Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default)
    {
        var review = await _grpcReviewService.GetReviewById(id, token);
        return review;
    }
}