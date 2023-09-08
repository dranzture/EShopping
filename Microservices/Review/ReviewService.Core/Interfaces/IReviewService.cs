﻿using ReviewService.Core.Dtos;
using ReviewService.Core.Models;

namespace ReviewService.Core.Interfaces;

public interface IReviewService
{
    Task AddReview(ReviewDto dto, CancellationToken token = default);
    
    Task UpdateReview(ReviewDto dto, CancellationToken token = default);

    Task DeleteReview(ReviewDto dto, CancellationToken token = default);
    
    Task<HashSet<Review>> GetReviewsByInventoryId(Guid id, CancellationToken token = default);

    Task<HashSet<Review>> GetReviewByUserId(int userId, CancellationToken token = default);
    
    Task<Review> GetReviewByUserIdAndInventoryId(Guid id, int userId, CancellationToken token = default);
}