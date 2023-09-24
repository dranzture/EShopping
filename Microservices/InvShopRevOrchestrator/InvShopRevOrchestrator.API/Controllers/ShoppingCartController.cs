using Microsoft.AspNetCore.Mvc;
using Grpc.Core;
using InvShopRevOrchestrator.API.Helpers;
using InvShopRevOrchestrator.Core.Dtos;
using InvShopRevOrchestrator.Core.Interfaces;

namespace InvShopRevOrchestrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<ShoppingCartController> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        [HttpPost("AddShoppingCart")]
        public async Task<IActionResult> AddShoppingCart([FromBody] ShoppingCartDto dto)
        {
            try
            {
                var cartId = await _shoppingCartService.AddShoppingCart(dto, CancellationToken.None);
                return Ok(new
                {
                    Id = cartId,
                    Message = "Shopping cart created successfully."
                });
            }
            catch (RpcException rpcEx)
            {
                return this.HandleRpcException(rpcEx, _logger);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, _logger);
            }
        }

        [HttpPut("UpdateShoppingItem")]
        public async Task<IActionResult> UpdateShoppingItem([FromBody] UpdateShoppingItemRequestDto request)
        {
            try
            {
                await _shoppingCartService.UpdateShoppingItem(request.ShoppingCartDto, request.InventoryDto,
                    request.Quantity, CancellationToken.None);
                return Ok("Shopping item updated successfully.");
            }
            catch (RpcException rpcEx)
            {
                return this.HandleRpcException(rpcEx, _logger);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, _logger);
            }
        }
        [HttpPut("AddShoppingItem")]
        public async Task<IActionResult> AddShoppingItem([FromBody] AddShoppingItemRequestDto request)
        {
            try
            {
                await _shoppingCartService.AddShoppingItem(request.ShoppingCartDto, request.InventoryDto, request.Quantity, CancellationToken.None);
                return Ok("Shopping item added successfully.");
            }
            catch (RpcException rpcEx)
            {
                return this.HandleRpcException(rpcEx, _logger);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, _logger);
            }
        }

        [HttpPut("DeleteShoppingItem")]
        public async Task<IActionResult> DeleteShoppingItem([FromBody] DeleteShoppingItemRequestDto request)
        {
            try
            {
                await _shoppingCartService.DeleteShoppingItem(request.ShoppingCartDto, request.InventoryDto, CancellationToken.None);
                return Ok("Shopping item deleted successfully.");
            }
            catch (RpcException rpcEx)
            {
                return this.HandleRpcException(rpcEx, _logger);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, _logger);
            }
        }

        [HttpPost("CheckoutShoppingCart")]
        public async Task<IActionResult> CheckoutShoppingCart([FromBody] ShoppingCartDto dto)
        {
            try
            {
                await _shoppingCartService.CheckoutShoppingCart(dto, CancellationToken.None);
                return Ok("Shopping cart checked out successfully.");
            }
            catch (RpcException rpcEx)
            {
                return this.HandleRpcException(rpcEx, _logger);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, _logger);
            }
        }

        [HttpGet("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails(Guid cartId)
        {
            try
            {
                var result = await _shoppingCartService.GetOrderDetails(cartId, CancellationToken.None);
                return Ok(result);
            }
            catch (RpcException rpcEx)
            {
                return this.HandleRpcException(rpcEx, _logger);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex, _logger);
            }
        }
    }
}
