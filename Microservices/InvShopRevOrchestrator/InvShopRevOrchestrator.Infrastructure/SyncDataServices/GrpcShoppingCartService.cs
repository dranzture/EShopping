using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcShoppingCartService;
using InvShopRevOrchestrator.Core.Dtos;
using InvShopRevOrchestrator.Core.Interfaces;
using InvShopRevOrchestrator.Core.ValueObjects;
using ShoppingCartServiceClient = GrpcShoppingCartService.GrpcShoppingCartService.GrpcShoppingCartServiceClient;

namespace InvShopRevOrchestrator.Infrastructure.SyncDataServices;

public class GrpcShoppingCartService : IGrpcShoppingCartService
{
    private readonly GrpcChannel _channel;
    private readonly IMapper _mapper;


    public GrpcShoppingCartService(AppSettings settings, IMapper mapper)
    {
        _mapper = mapper;
        _channel = GrpcChannel.ForAddress(settings.ShoppingCartUrl);
    }

    public async Task<Guid> AddShoppingCart(ShoppingCartDto request, CancellationToken token = default)
    {
        try
        {
            var client = new ShoppingCartServiceClient(_channel);
            var grpcRequest = _mapper.Map<GrpcShoppingCartDto>(request);

            var result = await client.AddShoppingCartAsync(grpcRequest, cancellationToken: token);

            return Guid.Parse(result.Value);
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? "---> Error on AddShoppingCartAsync: gRPC Client Timeout"
                : $"---> Error on AddShoppingCartAsync: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"---> Internal Error on AddShoppingCartAsync: {ex.Message}");
            throw;
        }
    }

    public async Task AddShoppingItem(AddShoppingCartItemCommandDto request, CancellationToken token = default)
    {
        try
        {
            var client = new ShoppingCartServiceClient(_channel);
            var grpcRequest = _mapper.Map<GrpcAddShoppingCartItemCommandDto>(request);

            await client.AddShoppingItemAsync(grpcRequest, cancellationToken: token);
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? "---> Error on AddShoppingItemAsync: gRPC Client Timeout"
                : $"---> Error on AddShoppingItemAsync: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"---> Internal Error on AddShoppingItemAsync: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateShoppingItem(UpdateShoppingCartItemCommandDto request,
        CancellationToken token = default)
    {
        try
        {
            var client = new ShoppingCartServiceClient(_channel);
            var grpcRequest = _mapper.Map<GrpcUpdateShoppingCartItemCommandDto>(request);

            await client.UpdateShoppingItemAsync(grpcRequest, cancellationToken: token);
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? "---> Error on UpdateShoppingItemAsync: gRPC Client Timeout"
                : $"---> Error on UpdateShoppingItemAsync: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"---> Internal Error on UpdateShoppingItemAsync: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteShoppingItem(DeleteShoppingCartItemCommandDto request,
        CancellationToken token = default)
    {
        try
        {
            var client = new ShoppingCartServiceClient(_channel);
            var grpcRequest = _mapper.Map<GrpcDeleteShoppingCartItemCommandDto>(request);

            await client.DeleteShoppingItemAsync(grpcRequest, cancellationToken: token);
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? "---> Error on DeleteShoppingItemAsync: gRPC Client Timeout"
                : $"---> Error on DeleteShoppingItemAsync: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"---> Internal Error on DeleteShoppingItemAsync: {ex.Message}");
            throw;
        }
    }

    public async Task CheckoutShoppingCart(Guid shoppingCartId, CancellationToken token = default)
    {
        try
        {
            var client = new ShoppingCartServiceClient(_channel);

            await client.CheckoutShoppingCartAsync(new StringValue()
            {
                Value = shoppingCartId.ToString()
            }, cancellationToken: token);
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? "---> Error on CheckoutShoppingCartAsync: gRPC Client Timeout"
                : $"---> Error on CheckoutShoppingCartAsync: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"---> Internal Error on CheckoutShoppingCartAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<ShoppingCartDto> GetOrderDetails(Guid id, CancellationToken token = default)
    {
        try
        {
            var client = new ShoppingCartServiceClient(_channel);
            var request = new GrpcOrderDetailsRequest { CartId = id.ToString() };

            var result = await client.GetOrderDetailsAsync(request, cancellationToken: token);

            // Map the gRPC response to your DTO
            return _mapper.Map<ShoppingCartDto>(result);
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? "---> Error on GetOrderDetailsAsync: gRPC Client Timeout"
                : $"---> Error on GetOrderDetailsAsync: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"---> Internal Error on GetOrderDetailsAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<ShoppingCartDto> GetShoppingCartByUsername(string username, CancellationToken token = default)
    {
        try
        {
            var client = new ShoppingCartServiceClient(_channel);

            var result = await client.GetShoppingCartByUsernameAsync(new StringValue()
            {
                Value = username
            }, cancellationToken: token);

            // Map the gRPC response to your DTO
            return _mapper.Map<ShoppingCartDto>(result);
        }
        catch (RpcException ex)
        {
            // Handle gRPC exceptions
            Console.WriteLine(ex.StatusCode == StatusCode.DeadlineExceeded
                ? "---> Error on GetOrderDetailsAsync: gRPC Client Timeout"
                : $"---> Error on GetOrderDetailsAsync: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"---> Internal Error on GetOrderDetailsAsync: {ex.Message}");
            throw;
        }
    }
}