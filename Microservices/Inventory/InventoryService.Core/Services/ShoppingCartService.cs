using AutoMapper;
using Grpc.Core;
using InventoryService.Core.Commands.ShoppingCartCommands;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Core.ValueObjects;

namespace InventoryService.Core.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IMapper _mapper;
    private readonly ICheckoutPublisher<ShoppingCart> _publisher;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, 
        IInventoryRepository inventoryRepository, 
        IMapper mapper, 
        ICheckoutPublisher<ShoppingCart> publisher)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _inventoryRepository = inventoryRepository;
        _mapper = mapper;
        _publisher = publisher;
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

    public async Task AddShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, int amount,string username, CancellationToken token = default)
    {
        var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartDto);
        var inventory = await _inventoryRepository.GetById(shoppingCart.Id, token);
        var command = new AddToShoppingCartCommand(_shoppingCartRepository, _inventoryRepository,shoppingCart, inventory, amount, username);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Requested shopping cart already exists."));
        }
            
        await command.Execute();
    }

    public async Task UpdateShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto,
        int amount, string username, CancellationToken token = default)
    {
        var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartDto);
        var inventory = _mapper.Map<Inventory>(inventoryDto);
        var command = new UpdateAmountShoppingItemCommand(_shoppingCartRepository, _inventoryRepository,shoppingCart, inventory,amount, username);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Requested shopping cart item cannot be updated."));
        }
            
        await command.Execute();
    }

    public async Task DeleteShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, string username, CancellationToken token = default)
    {
        var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartDto);
        var inventory = _mapper.Map<Inventory>(inventoryDto);
        var command = new DeleteFromShoppingCartCommand(_shoppingCartRepository, _inventoryRepository,shoppingCart, inventory, username);
        if (!await command.CanExecute())
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                "Requested shopping cart cannot delete item."));
        }
            
        await command.Execute();
    }

    public async Task CheckoutShoppingCart(ShoppingCartDto dto, string username, CancellationToken token = default)
    {
        var cart = _mapper.Map<ShoppingCart>(dto);
        var command = new CheckoutShoppingCart(_shoppingCartRepository, cart, _publisher);
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
}