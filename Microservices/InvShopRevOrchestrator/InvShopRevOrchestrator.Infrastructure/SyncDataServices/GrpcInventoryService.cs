using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcInventoryService;
using InvShopRevOrchestrator.Core.Dtos.Inventory;
using InvShopRevOrchestrator.Core.Interfaces;
using InvShopRevOrchestrator.Core.ValueObjects;
using InventoryServiceClient = GrpcInventoryService.GrpcInventoryService.GrpcInventoryServiceClient;

namespace InvShopRevOrchestrator.Infrastructure.SyncDataServices;

public class GrpcInventoryService : IGrpcInventoryService
{
    private readonly AppSettings _settings;
    private readonly IMapper _mapper;


    public GrpcInventoryService(AppSettings settings, IMapper mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }

    public async Task<StringValue> AddInventory(UpdateInventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);

            var request = _mapper.Map<GrpcUpdateInventoryDto>(dto);
            var result = await client.AddInventoryAsync(request, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);

            return new StringValue()
            {
                Value = result.Value
            };
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Add Inventory: Grpc Client Timeout"
                : $"---> Error on Add Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Add Inventory: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateInventory(UpdateInventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = _mapper.Map<GrpcUpdateInventoryDto>(dto);
            await client.UpdateInventoryAsync(request, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
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

    public async Task DeleteInventory(InventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var grpcUpdateInventoryDto = _mapper.Map<GrpcUpdateInventoryDto>(dto);
            await client.DeleteInventoryAsync(grpcUpdateInventoryDto, deadline: DateTime.UtcNow.AddSeconds(10),
                cancellationToken: token);
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

    public async Task IncreaseInventory(InventoryQuantityChangeDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = _mapper.Map<GrpcInventoryQuantityChangeDto>(dto);
            await client.IncreaseInventoryAsync(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            ;
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

    public async Task DecreaseInventory(InventoryQuantityChangeDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = _mapper.Map<GrpcInventoryQuantityChangeDto>(dto);
            await client.DecreaseInventoryAsync(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            ;
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

    public async Task<InventoryDto> GetById(StringValue id, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = new StringValue()
            {
                Value = id.ToString()
            };
            var result = await client.GetByIdAsync(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);

            return _mapper.Map<InventoryDto>(result);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Get By Id Inventory: Grpc Client Timeout"
                : $"---> Error on Get By Id Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Get By Id Inventory: {ex.Message}");
            throw;
        }
    }

    public async Task<InventoryDto> GetByName(StringValue name, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);
            var request = new StringValue()
            {
                Value = name.ToString()
            };
            var result = await client.GetByNameAsync(request, deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);

            return _mapper.Map<InventoryDto>(result);
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Get By Name Inventory: Grpc Client Timeout"
                : $"---> Error on Get By Name Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Get By Name Inventory: {ex.Message}");
            throw;
        }
    }

    public async Task<HashSet<InventoryListItemDto>> GetInventoryList(CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.InventoryUrl);
            var client = new InventoryServiceClient(channel);

            var results = await client.GetInventoryListAsync(new Empty(),deadline: DateTime.UtcNow.AddSeconds(10), cancellationToken: token);
            var returnItems = new HashSet<InventoryListItemDto>();
            foreach (var result in results.DtoList)
            {
                returnItems.Add(_mapper.Map<InventoryListItemDto>(result));
            }
            return returnItems;
        }
        catch (RpcException ex)
        {
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? $"---> Error on Get By Name Inventory: Grpc Client Timeout"
                : $"---> Error on Get By Name Inventory: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Error on Get By Name Inventory: {ex.Message}");
            throw;
        }
    }
}