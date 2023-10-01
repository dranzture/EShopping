using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcShoppingCartService;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Interfaces;

namespace ShoppingCartService.API.SyncDataServices.Grpc;

public class ShoppingCartGrpcService : GrpcShoppingCartService.GrpcShoppingCartService.GrpcShoppingCartServiceBase
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IMapper _mapper;
    private readonly ILogger<ShoppingCartGrpcService> _logger;

    public ShoppingCartGrpcService(IShoppingCartService shoppingCartService, IMapper mapper,
        ILogger<ShoppingCartGrpcService> logger)
    {
        _shoppingCartService = shoppingCartService;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<StringValue> AddShoppingCart(GrpcShoppingCartDto request,
        ServerCallContext context)
    {
        var username = request.Username;

        try
        {
            var cartId = await _shoppingCartService.AddShoppingCart(username);
            return new StringValue { Value = cartId };
        }
        catch (RpcException ex)
        {
            _logger.LogError($"Error in AddShoppingCart: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception in AddShoppingCart: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<Empty> AddShoppingItem(GrpcAddShoppingCartItemCommandDto request,
        ServerCallContext context)
    {
        var username = request.Username;
        var inventoryDto = _mapper.Map<InventoryDto>(request.Inventory);

        try
        {
            await _shoppingCartService.AddShoppingItem(new Guid(request.ShoppingCartId), inventoryDto, request.Quantity,
                username);
            return new Empty();
        }
        catch (RpcException ex)
        {
            _logger.LogError($"Error in AddShoppingItem: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception in AddShoppingItem: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<Empty> UpdateShoppingItem(GrpcUpdateShoppingCartItemCommandDto request,
        ServerCallContext context)
    {
        var username = request.Username;
        var inventoryDto = _mapper.Map<InventoryDto>(request.Inventory);

        try
        {
            await _shoppingCartService.UpdateShoppingItem(new Guid(request.ShoppingCartId), inventoryDto,
                request.Quantity, username);
            return new Empty();
        }
        catch (RpcException ex)
        {
            _logger.LogError($"Error in UpdateShoppingItem: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception in UpdateShoppingItem: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<Empty> DeleteShoppingItem(GrpcDeleteShoppingCartItemCommandDto request,
        ServerCallContext context)
    {
        var username = request.Username;
        var inventoryDto = _mapper.Map<InventoryDto>(request.Inventory);

        try
        {
            await _shoppingCartService.DeleteShoppingItem(new Guid(request.ShoppingCartId), inventoryDto, username);
            return new Empty();
        }
        catch (RpcException ex)
        {
            _logger.LogError($"Error in DeleteShoppingItem: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception in DeleteShoppingItem: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<Empty> CheckoutShoppingCart(StringValue shoppingCartId, ServerCallContext context)
    {
        try
        {
            await _shoppingCartService.CheckoutShoppingCart(new Guid(shoppingCartId.Value));
            return new Empty();
        }
        catch (RpcException ex)
        {
            _logger.LogError($"Error in CheckoutShoppingCart: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception in CheckoutShoppingCart: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GrpcShoppingCartDto> GetOrderDetails(GrpcOrderDetailsRequest request,
        ServerCallContext context)
    {
        try
        {
            var returnItem = await _shoppingCartService.GetOrderDetails(new Guid(request.CartId));
            return _mapper.Map<GrpcShoppingCartDto>(returnItem);
        }
        catch (RpcException ex)
        {
            _logger.LogError($"Error in CheckoutShoppingCart: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception in CheckoutShoppingCart: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GrpcShoppingCartDto> GetShoppingCartByUsername(StringValue request, ServerCallContext context)
    {
        try
        {
            var returnItem = await _shoppingCartService.GetShoppingCartByUsername(request.Value);
            return _mapper.Map<GrpcShoppingCartDto>(returnItem);
        }
        catch (RpcException ex)
        {
            _logger.LogError($"Error in GetShoppingCartByUsername: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception in GetShoppingCartByUsername: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}