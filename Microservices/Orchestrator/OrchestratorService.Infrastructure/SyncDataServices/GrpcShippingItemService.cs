using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcShippingItemService;
using OrchestratorService.Core.Dtos.ShippingItem;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;
using ShippingServiceClient = GrpcShippingItemService.GrpcShippingItemService.GrpcShippingItemServiceClient;

namespace OrchestratorService.Infrastructure.SyncDataServices;

public class GrpcShippingItemService : IGrpcShippingItemService
{
    private readonly AppSettings _settings;
    private readonly IMapper _mapper;


    public GrpcShippingItemService(AppSettings settings, IMapper mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }


    public async Task UpdateShippingItemStatus(UpdateShippingStatusDto request, CancellationToken token = default)
    {
        try
        {
            var dto = _mapper.Map<GrpcUpdateShippingStatusDto>(request);
            var channel = GrpcChannel.ForAddress(_settings.ShippingItemUrl);
            var client = new ShippingServiceClient(channel);
            var result = await client.UpdateShippingStatusAsync(dto, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Update Shipping Item Status: Grpc Client Timeout"
                : $"---> Error on Update Shipping Item Status: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }

    public async Task<HashSet<ShippingItemDto>> GetAllShippingItems(CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ShippingItemUrl);
            var client = new ShippingServiceClient(channel);
            var results = await client.GetAllShippingItemsAsync(new Empty(), deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            return _mapper.Map<HashSet<ShippingItemDto>>(results.Items);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Get All Shipping Items: Grpc Client Timeout"
                : $"---> Error on Get All Shipping Items: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }

    public async Task<ShippingItemDto?> GetShippingItemById(Guid id, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ShippingItemUrl);
            var client = new ShippingServiceClient(channel);
            var result = await client.GetShippingByIdAsync(new StringValue()
                {
                    Value = id.ToString()
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            return _mapper.Map<ShippingItemDto>(result);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Retrieving Shipping Item: Grpc Client Timeout"
                : $"---> Error on Retrieving Shipping Item: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }

    public async Task<ShippingItemDto?> GetShippingItemByOrderId(Guid orderId, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.ShippingItemUrl);
            var client = new ShippingServiceClient(channel);
            var result = await client.GetShippingByOrderIdAsync(new StringValue()
                {
                    Value = orderId.ToString()
                }, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            return _mapper.Map<ShippingItemDto>(result);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Retrieving Shipping Item: Grpc Client Timeout"
                : $"---> Error on Retrieving Shipping Item: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Order: {ex.Message}");
            throw;
        }
    }
}