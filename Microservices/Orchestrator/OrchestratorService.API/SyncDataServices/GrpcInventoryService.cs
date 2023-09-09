using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcInventoryService;
using Microsoft.OpenApi.Extensions;
using OrchestratorService.Core.Dtos.Inventory;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;
using InventoryServiceClient = GrpcInventoryService.GrpcInventoryService.GrpcInventoryServiceClient;

namespace OrchestratorService.API.SyncDataServices;

public class GrpcInventoryService : IGrpcInventoryService, IDisposable
{
    private readonly AppSettings _settings;
    private readonly IMapper _mapper;


    public GrpcInventoryService(AppSettings settings, IMapper mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }

    public async Task<Guid> AddInventory(MutateInventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            
            var request = _mapper.Map<GrpcMutateInventoryDto>(dto);
            var result = client.AddInventory(request, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
            
            return await Task.FromResult(new Guid(result.Id));
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Add Inventory: Grpc Client Timeout"
                : $"---> Error on Add Inventory: {ex.Status.Detail}");
            return await Task.FromException<Guid>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Add Inventory: {ex.Message}");
            return await Task.FromException<Guid>(ex);
        }
    }

    public Task UpdateInventory(MutateInventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = _mapper.Map<GrpcMutateInventoryDto>(dto);
            client.UpdateInventory(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            return Task.CompletedTask;
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Update Inventory: Grpc Client Timeout"
                : $"---> Error on Update Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Update Inventory: {ex.Message}");
            throw;
        }
    }

    public Task DeleteInventory(Guid id, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var grpcIdParam = new GrpcIdParam()
            {
                Id = id.ToString()
            };
            client.DeleteInventory(grpcIdParam, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            return Task.CompletedTask;
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Delete Inventory: Grpc Client Timeout"
                : $"---> Error on Delete Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Delete Inventory: {ex.Message}");
            throw;
        }
    }

    public Task IncreaseInventory(ChangeInventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = _mapper.Map<GrpcInventoryChangeDto>(dto);
            client.IncreaseInventory(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            return Task.CompletedTask;
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Increase Inventory: Grpc Client Timeout"
                : $"---> Error on Increase Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Increase Inventory: {ex.Message}");
            throw;
        }
    }

    public Task DecreaseInventory(ChangeInventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = _mapper.Map<GrpcInventoryChangeDto>(dto);
            client.DecreaseInventory(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            return Task.CompletedTask;
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Decrease Inventory: Grpc Client Timeout"
                : $"---> Error on Decrease Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Decrease Inventory: {ex.Message}");
            throw;
        }
    }

    public async Task<InventoryDto> GetById(Guid id, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = new GrpcIdParam()
            {
                Id = id.ToString()
            };
            var result = client.GetById(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            
            return await Task.FromResult(_mapper.Map<InventoryDto>(result));
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Get By Id Inventory: Grpc Client Timeout"
                : $"---> Error on Get By Id Inventory: {ex.Status.Detail}");
            return await Task.FromException<InventoryDto>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Get By Id Inventory: {ex.Message}");
            return await Task.FromException<InventoryDto>(ex);
        }
    }

    public async Task<InventoryDto> GetByName(string name, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = new GrpcNameParam()
            {
                Name = name
            };
            var result = client.GetByName(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            
            return await Task.FromResult(_mapper.Map<InventoryDto>(result));
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Get By Name Inventory: Grpc Client Timeout"
                : $"---> Error on Get By Name Inventory: {ex.Status.Detail}");
            return await Task.FromException<InventoryDto>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Get By Name Inventory: {ex.Message}");
            return await Task.FromException<InventoryDto>(ex);
        }
    }


    public void Dispose()
    {
        // _channel.ShutdownAsync();
        // _channel.Dispose();
    }
}