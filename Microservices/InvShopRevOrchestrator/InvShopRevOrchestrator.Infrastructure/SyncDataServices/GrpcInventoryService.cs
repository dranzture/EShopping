using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcInventoryService;
using InvShopRevOrchestrator.Core.Dtos;
using InvShopRevOrchestrator.Core.Interfaces;
using InvShopRevOrchestrator.Core.ValueObjects;
using InventoryServiceClient = GrpcInventoryService.GrpcInventoryService.GrpcInventoryServiceClient;

namespace InvShopRevOrchestrator.Infrastructure.SyncDataServices;

public class GrpcInventoryService : IGrpcInventoryService
{
    private readonly GrpcChannel _channel;
    private readonly IMapper _mapper;


    public GrpcInventoryService(AppSettings settings, IMapper mapper)
    {
        _mapper = mapper;
        _channel = GrpcChannel.ForAddress(settings.InventoryUrl);
    }

    public async Task<StringValue> AddInventory(InventoryDto dto, CancellationToken token = default)
    {
        try
        {
            var client = new InventoryServiceClient(_channel);

            var request = _mapper.Map<GrpcInventoryDto>(dto);
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

    public async Task UpdateInventory(InventoryWithUsernameDto dto, CancellationToken token = default)
    {
        try
        {
            var client = new InventoryServiceClient(_channel);
            var request = _mapper.Map<GrpcInventoryWithUsernameDto>(dto);
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
    
    public async Task DeleteInventory(InventoryWithUsernameDto dto, CancellationToken token = default)
    {
        try
        {
            var client = new InventoryServiceClient(_channel);
            var grpcInventoryDtoWithUsername = _mapper.Map<GrpcInventoryWithUsernameDto>(dto);
            await client.DeleteInventoryAsync(grpcInventoryDtoWithUsername, deadline: DateTime.UtcNow.AddSeconds(10),
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

    public async Task IncreaseInventory(InventoryQuantityChangeRequestDto baseDto, CancellationToken token = default)
    {
        try
        {
            var client = new InventoryServiceClient(_channel);
            var request = _mapper.Map<GrpcInventoryQuantityChangeDto>(baseDto);
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

    public async Task DecreaseInventory(InventoryQuantityChangeRequestDto baseDto, CancellationToken token = default)
    {
        try
        {
            var client = new InventoryServiceClient(_channel);
            var request = _mapper.Map<GrpcInventoryQuantityChangeDto>(baseDto);
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

    public async Task<InventoryDto> GetById(Guid id, CancellationToken token = default)
    {
        try
        {
            var client = new InventoryServiceClient(_channel);
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

    public async Task<InventoryDto> GetByName(string name, CancellationToken token = default)
    {
        try
        {
            var client = new InventoryServiceClient(_channel);
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
            var client = new InventoryServiceClient(_channel);

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