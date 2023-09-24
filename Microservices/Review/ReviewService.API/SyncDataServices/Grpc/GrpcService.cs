using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcReviewService;
using ReviewService.Core.Dtos;
using ReviewService.Core.Interfaces;
using Empty = GrpcReviewService.Empty;

namespace ReviewService.API.SyncDataServices.Grpc;

public class GrpcService : GrpcReviewService.GrpcReviewService.GrpcReviewServiceBase
{
    private readonly IMapper _mapper;
    private readonly IReviewService _reviewService;

    public GrpcService(IMapper mapper, IReviewService reviewService)
    {
        _mapper = mapper;
        _reviewService = reviewService;
    }

    public override async Task<StringValue> AddReview(GrpcReviewDto request, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<ReviewDto>(request);
            var id = await _reviewService.AddReview(item);
            return new StringValue()
            {
                Value = id
            };
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"---> Could not add review due to: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not add review due to: {ex.Message}");

            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }

    public override async Task<Empty> UpdateReview(GrpcReviewDto request, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<ReviewDto>(request);
            await _reviewService.UpdateReview(item);
            return new Empty();
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"---> Could not update review due to: {ex.Message}");
            
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not update review due to: {ex.Message}");
            
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }

    public override async Task<Empty> DeleteReview(GrpcReviewDto request, ServerCallContext context)
    {
        try
        {
            var item = _mapper.Map<ReviewDto>(request);
            await _reviewService.DeleteReview(item);
            return new Empty();
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"---> Could not delete review due to: {ex.Message}");
            
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not delete review due to: {ex.Message}");
            
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }

    public override async Task<GrpcListedReviews> GetReviewsByUserId(GrpcUserId request, ServerCallContext context)
    {
        try
        {
            var returnItem = new GrpcListedReviews();
            var list = await _reviewService.GetReviewsByUserId(request.UserId);

            foreach (var item in list)
            {
                returnItem.Dto.Add(_mapper.Map<GrpcReviewDto>(item));
            }

            return returnItem;
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"---> Could not get reviews due to: {ex.Message}");
            
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get reviews due to: {ex.Message}");
            
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }

    public override async Task<GrpcListedReviews> GetReviewsByInventoryId(StringValue request,
        ServerCallContext context)
    {
        try
        {
            var returnItem = new GrpcListedReviews();
            var list = await _reviewService.GetReviewsByInventoryId(new Guid(request.Value));
            foreach (var item in list)
            {
                returnItem.Dto.Add(_mapper.Map<GrpcReviewDto>(item));
            }

            return returnItem;
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"---> Could not get reviews due to: {ex.Message}");
            
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get reviews due to: {ex.Message}");
            
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }

    public override async Task<GrpcReviewDto> GetReviewByInventoryIdAndUserId(GrpcUserAndInventoryId request,
        ServerCallContext context)
    {
        try
        {
            var review =
                await _reviewService.GetReviewByInventoryIdAndUserId(new Guid(request.InventoryId), request.UserId);

            return _mapper.Map<GrpcReviewDto>(review);
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"---> Could not get review due to: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get review due to: {ex.Message}");
            
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }

    public override async Task<GrpcReviewDto?> GetReviewById(StringValue request, ServerCallContext context)
    {
        try
        {
            var review =
                await _reviewService.GetReviewById(new Guid(request.Value));

            return _mapper.Map<GrpcReviewDto>(review);
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"---> Could not get review due to: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Could not get review due to: {ex.Message}");
            
            throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
        }
    }
}