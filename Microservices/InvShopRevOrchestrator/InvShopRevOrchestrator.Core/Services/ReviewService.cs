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

    public async Task<Guid> AddReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var inventory = await _grpcInventoryService.GetById(dto.InventoryId, token);
            if (inventory == null)
            {
                throw new ArgumentException("Inventory not found");
            }

            var reviewId = await _grpcReviewService.AddReview(dto, token);
            return reviewId;
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var review = await _grpcReviewService.GetReviewById(dto.Id, token);
            if (review == null)
            {
                throw new ArgumentException("Review not found");
            }

            await _grpcReviewService.UpdateReview(dto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var review = await _grpcReviewService.GetReviewById(dto.Id, token);
            if (review == null)
            {
                throw new ArgumentException("Review not found");
            }

            await _grpcReviewService.DeleteReview(dto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default)
    {
        try
        {
            var reviews = await _grpcReviewService.GetReviewsByInventoryId(id, token);
            return reviews;
        }
        catch
        {
            throw;
        }
    }

    public async Task<HashSet<ReviewDto>> GetReviewsByUserId(int userId, CancellationToken token = default)
    {
        try
        {
            var reviews = await _grpcReviewService.GetReviewsByUserId(userId, token);
            return reviews;
        }
        catch
        {
            throw;
        }
    }

    public async Task<ReviewDto?> GetReviewByUserIdAndInventoryId(Guid id, int userId, CancellationToken token = default)
    {
        try
        {
            var review = await _grpcReviewService.GetReviewByInventoryIdAndUserId(id,userId, token);
            return review;
        }
        catch
        {
            throw;
        }
    }

    public async Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default)
    {
        try
        {
            var review = await _grpcReviewService.GetReviewById(id, token);
            return review;
        }
        catch
        {
            throw;
        }
    }
}