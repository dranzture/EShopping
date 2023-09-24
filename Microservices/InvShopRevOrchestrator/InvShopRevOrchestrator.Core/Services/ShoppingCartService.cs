
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

    public async Task AddShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, int quantity,
        CancellationToken token = default)
    {
        try
        {
            var inventory = await _grpcInventoryService.GetById(inventoryDto.Id, token);
            if (inventory == null)
            {
                throw new ArgumentException("Inventory not found");
            }

            var newAddShoppingItemDto = new AddShoppingCartItemCommandDto
            {
                ShoppingCart = shoppingCartDto,
                Inventory = inventory,
                Quantity = quantity
            };
            await _grpcShoppingCartService.AddShoppingItem(newAddShoppingItemDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto, int quantity,
        CancellationToken token = default)
    {
        try
        {
            var newUpdateShoppingItemDto = new UpdateShoppingCartItemCommandDto
            {
                ShoppingCart = shoppingCartDto,
                Inventory = inventoryDto,
                Quantity = quantity
            };
            await _grpcShoppingCartService.UpdateShoppingItem(newUpdateShoppingItemDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteShoppingItem(ShoppingCartDto shoppingCartDto, InventoryDto inventoryDto,
        CancellationToken token = default)
    {
        try
        {
            var newDeleteShoppingItemDto = new DeleteShoppingCartItemCommandDto()
            {
                ShoppingCart = shoppingCartDto,
                Inventory = inventoryDto,
            };
            await _grpcShoppingCartService.DeleteShoppingItem(newDeleteShoppingItemDto, token);
        }
        catch
        {
            throw;
        }
    }

    public async Task CheckoutShoppingCart(ShoppingCartDto dto, CancellationToken token = default)
    {
        try
        {
            await _grpcShoppingCartService.CheckoutShoppingCart(dto, token);
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