using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ReviewService.Core.Commands.ReviewCommands;
using ReviewService.Core.Dtos;
using ReviewService.Core.Interfaces;
using ReviewService.Core.Models;

namespace ReviewService.Core.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    
    
    public ReviewService(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> AddReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var review = _mapper.Map<Review>(dto);
            var addCommand = new AddReviewCommand(_repository, review);
            if (!await addCommand.CanExecute())
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Cannot add review."));
            await addCommand.Execute();
            return addCommand.GetResult()!.Id.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not add review due to: {ex.Message}");
            throw;
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

    public async Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default)
    {
        var result = await Task.Run(() => _repository.Queryable(token).Where(e => e.InventoryId == id).ToHashSet(), token);
        return _mapper.Map<HashSet<ReviewDto>>(result);
    }

    public async Task<HashSet<ReviewDto>> GetReviewsByUserId(int userId, CancellationToken token = default)
    {
        var result = await _repository.GetByUserId(userId, token);
        return _mapper.Map<HashSet<ReviewDto>>(result);
    }

    public async Task<ReviewDto?> GetReviewByInventoryIdAndUserId(Guid id, int userId,
        CancellationToken token = default)
    {
        var result = await _repository.GetByInventoryIdAndUserId(id ,userId, token);
        return _mapper.Map<ReviewDto>(result);
    }

    public async Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default)
    {
        var result = await _repository.Queryable(token).FirstOrDefaultAsync(e=>e.Id == id, token);
        return _mapper.Map<ReviewDto>(result);
    }
}