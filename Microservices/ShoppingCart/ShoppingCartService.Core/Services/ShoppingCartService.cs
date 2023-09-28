using AutoMapper;
using Grpc.Core;
using ShoppingCartService.Core.Commands;
using ShoppingCartService.Core.Dtos;
using ShoppingCartService.Core.Entities;
using ShoppingCartService.Core.Interfaces;

namespace ShoppingCartService.Core.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher<ShoppingCartDto> _shoppingCartPublisher;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
        IMapper mapper,
        IPublisher<ShoppingCartDto> shoppingCartPublisher
    )
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
        _shoppingCartPublisher = shoppingCartPublisher;
    }

    public async Task<string> AddShoppingCart(ShoppingCartDto dto, string username, CancellationToken token = default)
    {
        var shoppingCart = _mapper.Map<ShoppingCart>(dto);
        var command = new AddShoppingCart(_shoppingCartRepository, shoppingCart);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Requested shopping cart already exists."));
        }

        await command.Execute();
        return command.GetResult()!.Id.ToString();
    }

    public async Task AddShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, int quantity, string username,
        CancellationToken token = default)
    {
        var inventory = _mapper.Map<Inventory>(inventoryDto);
        var command =
            new AddToShoppingCartCommand(_shoppingCartRepository, shoppingCartId, inventory, quantity, username);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Bad request. Cannot add item to shopping cart."));
        }

        await command.Execute();
    }

    public async Task UpdateShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto,
        int quantity, string username, CancellationToken token = default)
    {
        var inventory = _mapper.Map<Inventory>(inventoryDto);
        var command =
            new UpdateQuantityShoppingItemCommand(_shoppingCartRepository, shoppingCartId, inventory, quantity,
                username);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Requested shopping cart item cannot be updated."));
        }

        await command.Execute();
    }

    public async Task DeleteShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, string username,
        CancellationToken token = default)
    {
        var inventory = _mapper.Map<Inventory>(inventoryDto);
        var command = new DeleteFromShoppingCartCommand(_shoppingCartRepository, shoppingCartId, inventory, username);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Requested shopping cart cannot delete item."));
        }

        await command.Execute();
    }

    public async Task CheckoutShoppingCart(Guid shoppingCartId, CancellationToken token = default)
    {
        var command = new CheckoutShoppingCartCommand(_shoppingCartRepository, shoppingCartId, _shoppingCartPublisher);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Requested shopping cart cannot be checked out."));
        }

        await command.Execute();
    }

    public async Task<ShoppingCartDto> GetOrderDetails(Guid cartId, CancellationToken token = default)
    {
        var result = await _shoppingCartRepository.GetShoppingCartById(cartId, token);
        return _mapper.Map<ShoppingCartDto>(result);
    }

    public async Task<ShoppingCartDto> GetShoppingCartByUsername(string username, CancellationToken token = default)
    {
        var result = await _shoppingCartRepository.GetShoppingCartByUsername(username, token);
        return _mapper.Map<ShoppingCartDto>(result);
    }
}