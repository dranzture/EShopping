using AutoMapper;
using Grpc.Core;
using InventoryService.Core.Commands.ShoppingCartCommands;
using InventoryService.Core.Dtos;
using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;

namespace InventoryService.Core.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
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
        return "";
    }

    public Task UpdateShoppingCart(ShoppingCartDto dto, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteShoppingCart(Guid id, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingItemDto> AddShoppingItem(ShoppingItemDto dto, Guid id, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingItemDto> UpdateShoppingItem(Guid id, int amount, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingItemDto> DeleteShoppingItem(Guid id, string username, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
    
}