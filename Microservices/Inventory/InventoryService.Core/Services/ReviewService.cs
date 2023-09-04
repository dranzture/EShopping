using AutoMapper;
using Grpc.Core;
using InventoryService.Core.Commands.ReviewCommands;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public ReviewService(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task AddReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var review = _mapper.Map<Review>(dto);
            var addCommand = new AddReviewCommand(_repository, review);
            if (!await addCommand.CanExecute())
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Cannot add review."));
            await addCommand.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not add review due to: {ex.Message}");
        }
        
    }

    public async Task UpdateReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var review = _mapper.Map<Review>(dto);
            var updateCommand = new UpdateReviewCommand(_repository, review, dto.Username);
            if (!await updateCommand.CanExecute())
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Cannot update review."));
            await updateCommand.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not update review due to: {ex.Message}");
        }
        
    }

    public async Task DeleteReview(ReviewDto dto, CancellationToken token = default)
    {
        var review = _mapper.Map<Review>(dto);
        var deleteCommand = new DeleteReviewCommand(_repository, review, dto.Username);
        if (!await deleteCommand.CanExecute())
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Cannot delete review."));
        await deleteCommand.Execute();
    }

    public async Task<HashSet<Review>> GetReviewsByInventoryId(ReviewDto dto, CancellationToken token = default)
    {
        return await Task.Run(() => _repository.Queryable(token).Where(e => e.InventoryId == dto.InventoryId).ToHashSet(), token);
    }

    public async Task<HashSet<Review>> GetReviewByUserId(int userId, CancellationToken token = default)
    {
        return await _repository.GetByUserId(userId, token);
    }

    public async Task<Review> GetReviewByUserIdAndInventoryId(Guid id, int userId,
        CancellationToken token = default)
    {
        return await _repository.GetByUserIdAndInventoryId(id ,userId, token);
    }
}