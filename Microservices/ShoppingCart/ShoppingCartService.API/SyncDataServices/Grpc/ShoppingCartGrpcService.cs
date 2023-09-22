using System;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcShoppingCartService;
using Microsoft.Extensions.Logging;
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
        var dto = _mapper.Map<ShoppingCartDto>(request);

        try
        {
            var cartId = await _shoppingCartService.AddShoppingCart(dto, username);
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
        var username = request.ShoppingCart.Username;
        var shoppingCartDto = _mapper.Map<ShoppingCartDto>(request.ShoppingCart);
        var inventoryDto = _mapper.Map<InventoryDto>(request.Inventory);

        try
        {
            await _shoppingCartService.AddShoppingItem(shoppingCartDto, inventoryDto, request.Quantity, username);
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
        var username = request.ShoppingCart.Username;
        var shoppingCartDto = _mapper.Map<ShoppingCartDto>(request.ShoppingCart);
        var inventoryDto = _mapper.Map<InventoryDto>(request.Inventory);

        try
        {
            await _shoppingCartService.UpdateShoppingItem(shoppingCartDto, inventoryDto, request.Quantity, username);
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
        var username = request.ShoppingCart.Username;
        var shoppingCartDto = _mapper.Map<ShoppingCartDto>(request.ShoppingCart);
        var inventoryDto = _mapper.Map<InventoryDto>(request.Inventory);

        try
        {
            await _shoppingCartService.DeleteShoppingItem(shoppingCartDto, inventoryDto, username);
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

    public override async Task<Empty> CheckoutShoppingCart(GrpcShoppingCartDto request, ServerCallContext context)
    {
        var username = request.Username;
        var shoppingCartDto = _mapper.Map<ShoppingCartDto>(request);

        try
        {
            await _shoppingCartService.CheckoutShoppingCart(shoppingCartDto, username);
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
}