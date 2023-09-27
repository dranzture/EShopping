
using InvShopRevOrchestrator.Core.Dtos;
using InvShopRevOrchestrator.Core.Interfaces;

namespace InvShopRevOrchestrator.Core.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IGrpcShoppingCartService _grpcShoppingCartService;
    private readonly IGrpcInventoryService _grpcInventoryService;
    
    public ShoppingCartService(IGrpcShoppingCartService grpcShoppingCartService, IGrpcInventoryService grpcInventoryService)
    {
        _grpcShoppingCartService = grpcShoppingCartService;
        _grpcInventoryService = grpcInventoryService;
    }
    
    public async Task<Guid> AddShoppingCart(ShoppingCartDto dto, CancellationToken token = default)
    {
        try
        {
            var cartId = await _grpcShoppingCartService.AddShoppingCart(dto, token);
            return cartId;
        }
        catch
        {
            throw;
        }
    }

    public async Task AddShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, int quantity, string username, 
        CancellationToken token = default)
    {
        try
        {
            var inventory = await _grpcInventoryService.GetById(inventoryDto.Id.Value, token);
            if (inventory == null)
            {
                throw new ArgumentException("Inventory not found");
            }

            var newAddShoppingItemDto = new AddShoppingCartItemCommandDto
            {
                ShoppingCartId = shoppingCartId,
                Inventory = inventory,
                Quantity = quantity,
                Username = username
            };
            await _grpcShoppingCartService.AddShoppingItem(newAddShoppingItemDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, int quantity, string username, 
        CancellationToken token = default)
    {
        try
        {
            var newUpdateShoppingItemDto = new UpdateShoppingCartItemCommandDto
            {
                ShoppingCartId = shoppingCartId,
                Inventory = inventoryDto,
                Quantity = quantity,
                Username = username
            };
            await _grpcShoppingCartService.UpdateShoppingItem(newUpdateShoppingItemDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteShoppingItem(Guid shoppingCartId, InventoryDto inventoryDto, string username, 
        CancellationToken token = default)
    {
        try
        {
            var newDeleteShoppingItemDto = new DeleteShoppingCartItemCommandDto()
            {
                ShoppingCartId = shoppingCartId,
                Inventory = inventoryDto,
                Username = username
            };
            await _grpcShoppingCartService.DeleteShoppingItem(newDeleteShoppingItemDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task CheckoutShoppingCart(Guid shoppingCartId, CancellationToken token = default)
    {
        try
        {
            await _grpcShoppingCartService.CheckoutShoppingCart(shoppingCartId, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task<ShoppingCartDto> GetShoppingCartByUsername(string username, CancellationToken token = default)
    {
        try
        {
            var result = await _grpcShoppingCartService.GetShoppingCartByUsername(username, token);
            return result;
        }
        catch
        {
            throw;
        }
    }

    public async Task<ShoppingCartDto> GetOrderDetails(Guid cartId, CancellationToken token = default)
    {
        try
        {
            var result = await _grpcShoppingCartService.GetOrderDetails(cartId, token);
            return result;
        }
        catch
        {
            throw;
        }
    }
}