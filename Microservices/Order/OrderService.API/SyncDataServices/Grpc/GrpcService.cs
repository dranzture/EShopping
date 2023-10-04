using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcOrderService;
using OrderService.Core.Interfaces;

namespace OrderService.API.SyncDataServices.Grpc;

public class GrpcService : GrpcOrderService.GrpcOrderService.GrpcOrderServiceBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly ILogger<GrpcService> _logger;

    public GrpcService(IOrderService orderService, IMapper mapper,
        ILogger<GrpcService> logger)
    {
        _orderService = orderService;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<Empty> ReprocessOrderById(StringValue request,
        ServerCallContext context)
    {
        try
        {
            await _orderService.ReprocessOrderById(new Guid(request.Value), context.CancellationToken);
            return new Empty();
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Reprocessing Order: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in Reprocessing Order: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GrpcOrderDto> GetOrderById(StringValue request,
        ServerCallContext context)
    {
        try
        {
            var result = await _orderService.GetOrderById(new Guid(request.Value), context.CancellationToken);
            return _mapper.Map<GrpcOrderDto>(result);
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Reprocessing Order: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in Reprocessing Order: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GrpcListOrderDto> GetAllOrders(Empty request, ServerCallContext context)
    {
        try
        {
            var results = await _orderService.GetAllOrders(context.CancellationToken);
            var returnItem = new GrpcListOrderDto();
            foreach (var result in results)
            {
                returnItem.Orders.Add(_mapper.Map<GrpcOrderDto>(result));
            }
            return returnItem;
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Getting All Orders: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in GetAllOrders: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
    
    public override async Task<GrpcListOrderDto> GetOrdersByUsername(StringValue request, ServerCallContext context)
    {
        try
        {
            var results = await _orderService.GetOrdersByUsername(request.Value, context.CancellationToken);
            var returnItem = new GrpcListOrderDto();
            foreach (var result in results)
            {
                returnItem.Orders.Add(_mapper.Map<GrpcOrderDto>(result));
            }
            return returnItem;
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error in Getting Orders By Username: {Ex}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled exception in GetOrdersByUsername: {Ex}", ex);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}