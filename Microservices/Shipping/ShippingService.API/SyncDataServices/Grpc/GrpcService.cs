using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcShippingItemService;
using ShippingService.Core.Dto;
using ShippingService.Core.Interfaces;

namespace ShippingService.API.SyncDataServices.Grpc;

public class GrpcService : GrpcShippingItemService.GrpcShippingItemService.GrpcShippingItemServiceBase
{
    private readonly IShippingItemService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<GrpcService> _logger;

    public GrpcService(IShippingItemService service, IMapper mapper, ILogger<GrpcService> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<Empty> UpdateShippingStatus(GrpcUpdateShippingStatusDto request,
        ServerCallContext context)
    {
        try
        {
            var shippingItem = _mapper.Map<ShippingItemDto>(request);
            await _service.UpdateStatus(shippingItem, context.CancellationToken);
            return new Empty();
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Updating Shipping: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in Updating Shipping: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<ListGrpcShippingItemDto> GetAllShippingItems(Empty request, ServerCallContext context)
    {
        try
        {
            var results = await _service.GetAllShippingItems(context.CancellationToken);
            var returnItems = new ListGrpcShippingItemDto();
            foreach (var result in results)
            {
                returnItems.Items.Add(_mapper.Map<GrpcShippingItemDto>(result));
            }

            return returnItems;
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Updating Shipping: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in Updating Shipping: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GrpcShippingItemDto> GetShippingById(StringValue request,
        ServerCallContext context)
    {
        try
        {
            var shippingItem = await _service.GetShippingItemById(new Guid(request.Value), context.CancellationToken);
            return _mapper.Map<GrpcShippingItemDto>(shippingItem);
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Updating Shipping: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in Updating Shipping: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
    
    public override async Task<GrpcShippingItemDto> GetShippingByOrderId(StringValue request,
        ServerCallContext context)
    {
        try
        {
            var shippingItem = await _service.GetShippingItemByOrderId(new Guid(request.Value), context.CancellationToken);
            return _mapper.Map<GrpcShippingItemDto>(shippingItem);
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Updating Shipping: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in Updating Shipping: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}