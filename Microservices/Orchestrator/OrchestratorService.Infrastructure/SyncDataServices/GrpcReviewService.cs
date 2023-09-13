using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcInventoryService;
using GrpcReviewService;
using OrchestratorService.Core.Dtos.Review;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;
using ReviewServiceClient = GrpcReviewService.GrpcReviewService.GrpcReviewServiceClient;

namespace OrchestratorService.Infrastructure.SyncDataServices;

public class GrpcReviewService : IGrpcReviewService
{
    private readonly AppSettings _settings;
    private readonly IMapper _mapper;


    public GrpcReviewService(AppSettings settings, IMapper mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }

    public Task<Guid> AddReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ReviewUrl);
            var client = new ReviewServiceClient(channel);
        
            var request = new GrpcReviewDto
            {
                Id = dto.Id.ToString(),
                InventoryId = dto.InventoryId.ToString(),
                ExternalUserId = dto.ExternalUserId,
                Username = dto.Username,
                Stars = dto.Stars,
                Comment = dto.Comment
        
            };
            var result = client.AddReview(request, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            
            return Task.FromResult(new Guid(result.Value));
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Add Review: Grpc Client Timeout"
                : $"---> Error on Add Review: {ex.Status.Detail}");
            return Task.FromException<Guid>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Add Review: {ex.Message}");
            return Task.FromException<Guid>(ex);
        }
    }

    public Task UpdateReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ReviewUrl);
            var client = new ReviewServiceClient(channel);
        
            var request = new GrpcReviewDto
            {
                Id = dto.Id.ToString(),
                InventoryId = dto.InventoryId.ToString(),
                ExternalUserId = dto.ExternalUserId,
                Username = dto.Username,
                Stars = dto.Stars,
                Comment = dto.Comment
        
            };
            var result = client.UpdateReview(request, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);

            return Task.CompletedTask;
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Update Review: Grpc Client Timeout"
                : $"---> Error on Update Review: {ex.Status.Detail}");
            return Task.FromException<Guid>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Review: {ex.Message}");
            return Task.FromException<Guid>(ex);
        }
    }

    public Task DeleteReview(ReviewDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ReviewUrl);
            var client = new ReviewServiceClient(channel);
        
            var request = new GrpcReviewDto
            {
                Id = dto.Id.ToString(),
                InventoryId = dto.InventoryId.ToString(),
                ExternalUserId = dto.ExternalUserId,
                Username = dto.Username,
                Stars = dto.Stars,
                Comment = dto.Comment
        
            };
            var result = client.DeleteReview(request, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);

            return Task.CompletedTask;
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Delete Review: Grpc Client Timeout"
                : $"---> Error on Delete Review: {ex.Status.Detail}");
            return Task.FromException<Guid>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Delete Review: {ex.Message}");
            return Task.FromException<Guid>(ex);
        }
    }

    public Task<HashSet<ReviewDto>> GetReviewsByInventoryId(Guid id, CancellationToken token = default)
    {
        try
        {
            var returnItem = new HashSet<ReviewDto>();
            var channel = GrpcChannel.ForAddress(_settings.ReviewUrl);
            var client = new ReviewServiceClient(channel);

            var result = client.GetReviewsByInventoryId(new StringValue()
                {
                    Value = id.ToString()
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);

            foreach (var item in result.Dto)
            {
                returnItem.Add(new ReviewDto()
                {
                    Comment = item.Comment,
                    ExternalUserId = item.ExternalUserId,
                    Id = Guid.Parse(item.Id),
                    InventoryId = Guid.Parse(item.InventoryId),
                    Stars = item.Stars,
                    Username = item.Username
                });
            }

            return Task.FromResult(returnItem);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on GetReviewsByInventoryId: Grpc Client Timeout"
                : $"---> Error on GetReviewsByInventoryId: {ex.Status.Detail}");
            return Task.FromException<HashSet<ReviewDto>>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on GetReviewsByInventoryId: {ex.Message}");
            return Task.FromException<HashSet<ReviewDto>>(ex);
        }
    }

    public Task<HashSet<ReviewDto>> GetReviewsByUserId(int userId, CancellationToken token = default)
    {
        try
        {
            var returnItem = new HashSet<ReviewDto>();
            var channel = GrpcChannel.ForAddress(_settings.ReviewUrl);
            var client = new ReviewServiceClient(channel);

            var result = client.GetReviewsByUserId(new GrpcUserId()
                {
                    UserId = userId
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);

            foreach (var item in result.Dto)
            {
                returnItem.Add(new ReviewDto()
                {
                    Comment = item.Comment,
                    ExternalUserId = item.ExternalUserId,
                    Id = Guid.Parse(item.Id),
                    InventoryId = Guid.Parse(item.InventoryId),
                    Stars = item.Stars,
                    Username = item.Username
                });
            }

            return Task.FromResult(returnItem);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on GetReviewsByUserId: Grpc Client Timeout"
                : $"---> Error on DGetReviewsByUserId: {ex.Status.Detail}");
            return Task.FromException<HashSet<ReviewDto>>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on GetReviewsByUserId: {ex.Message}");
            return Task.FromException<HashSet<ReviewDto>>(ex);
        }
    }

    public Task<ReviewDto?> GetReviewByUserIdAndInventoryId(Guid id, int userId, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ReviewUrl);
            var client = new ReviewServiceClient(channel);

            var result = client.GetReviewByUserIdAndInventoryId(new GrpcUserAndInventoryId()
                {
                    UserId = userId,
                    InventoryId = id.ToString()
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);

            return Task.FromResult(_mapper.Map<ReviewDto>(result));
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on GetReviewByUserIdAndInventoryId: Grpc Client Timeout"
                : $"---> Error on GetReviewByUserIdAndInventoryId: {ex.Status.Detail}");
            return Task.FromException<ReviewDto?>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on GetReviewByUserIdAndInventoryId: {ex.Message}");
            return Task.FromException<ReviewDto?>(ex);
        }
    }

    public Task<ReviewDto?> GetReviewById(Guid id, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ReviewUrl);
            var client = new ReviewServiceClient(channel);

            var result = client.GetReviewById(new StringValue()
                {
                    Value = id.ToString()
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);

            return Task.FromResult(_mapper.Map<ReviewDto>(result));
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on GetReviewById: Grpc Client Timeout"
                : $"---> Error on GetReviewById: {ex.Status.Detail}");
            return Task.FromException<ReviewDto?>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on GetReviewById: {ex.Message}");
            return Task.FromException<ReviewDto?>(ex);
        }
    }
}