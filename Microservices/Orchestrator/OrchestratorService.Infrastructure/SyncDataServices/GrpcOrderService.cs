using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using OrchestratorService.Core.Dtos.Order;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;
using OrderServiceClient = GrpcOrderService.GrpcOrderService.GrpcOrderServiceClient;

namespace OrchestratorService.Infrastructure.SyncDataServices;

public class GrpcOrderService : IGrpcOrderService
{
    private readonly AppSettings _settings;
    private readonly IMapper _mapper;


    public GrpcOrderService(AppSettings settings, IMapper mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }


    public async Task ReprocessOrderById(Guid id, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.OrderUrl);
            var client = new OrderServiceClient(channel);
            await client.ReprocessOrderByIdAsync(new StringValue()
                {
                    Value = id.ToString()
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Update Order: Grpc Client Timeout"
                : $"---> Error on Update Order: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }
    

    public async Task<HashSet<OrderDto>> GetAllOrders(CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.OrderUrl);
            var client = new OrderServiceClient(channel);
            var results = await client.GetAllOrdersAsync(new Empty(), deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            return _mapper.Map<HashSet<OrderDto>>(results.Orders);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Getting All Orders: Grpc Client Timeout"
                : $"---> Error on Getting All Orders: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }

    public async Task<HashSet<OrderDto>> GetOrdersByUsername(string username, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.OrderUrl);
            var client = new OrderServiceClient(channel);
            var result = await client.GetOrdersByUsernameAsync(new StringValue()
                {
                    Value = username
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            return _mapper.Map<HashSet<OrderDto>>(result.Orders);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Getting Order By Username: Grpc Client Timeout"
                : $"---> Error on Getting Orders By Username: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }

    public async Task<OrderDto?> GetByOrderId(Guid orderId, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.OrderUrl);
            var client = new OrderServiceClient(channel);
            var result = await client.GetOrderByIdAsync(new StringValue()
                {
                    Value = orderId.ToString()
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            return _mapper.Map<OrderDto>(result);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Getting Order By Id: Grpc Client Timeout"
                : $"---> Error on Getting Order By Id: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }
}