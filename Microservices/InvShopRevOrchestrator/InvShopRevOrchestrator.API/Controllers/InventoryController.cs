using Grpc.Core;
using InvShopRevOrchestrator.API.Helpers;
using InvShopRevOrchestrator.Core.Dtos;
using InvShopRevOrchestrator.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvShopRevOrchestrator.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
    {
        _inventoryService = inventoryService;
        _logger = logger;
    }

    [HttpPost("AddInventory")]
    public async Task<IActionResult> AddInventory([FromBody] InventoryDto dto)
    {
        try
        {
            var username = HttpContext.User.Identity.Name; // Get the username from the authenticated user
            var result = await _inventoryService.AddInventory(dto, username, CancellationToken.None);

            return Ok(new
            {
                Id = result,
                Message = "Inventory added successfully"
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

    [HttpPut("UpdateInventory")]
    public async Task<IActionResult> UpdateInventory([FromBody] InventoryDto dto)
    {
        try
        {
            var username = HttpContext.User.Identity.Name; // Get the username from the authenticated user
            await _inventoryService.UpdateInventory(dto, username, CancellationToken.None);

            _logger.LogInformation("Inventory updated successfully");
            return Ok("Inventory updated successfully.");
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

    [HttpPut("DecreaseInventory")]
    public async Task<IActionResult> DecreaseInventory([FromBody] InventoryQuantityChangeBaseDto dto)
    {
        try
        {
            var username = HttpContext.User.Identity.Name; // Get the username from the authenticated user
            await _inventoryService.DecreaseInventory(dto, username, CancellationToken.None);

            _logger.LogInformation("Inventory decreased successfully");
            return Ok("Inventory decreased successfully.");
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

    [HttpPut("IncreaseInventory")]
    public async Task<IActionResult> IncreaseInventory([FromBody] InventoryQuantityChangeBaseDto dto)
    {
        try
        {
            var username = HttpContext.User.Identity.Name; // Get the username from the authenticated user
            await _inventoryService.IncreaseInventory(dto, username, CancellationToken.None);

            _logger.LogInformation("Inventory increased successfully");
            return Ok("Inventory increased successfully.");
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

    [HttpDelete("DeleteInventory")]
    public async Task<IActionResult> DeleteInventory([FromBody] InventoryDto dto)
    {
        try
        {
            var username = HttpContext.User.Identity.Name; // Get the username from the authenticated user
            await _inventoryService.DeleteInventory(dto, username, CancellationToken.None);

            _logger.LogInformation("Inventory deleted successfully");
            return Ok("Inventory deleted successfully.");
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

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var inventory = await _inventoryService.GetById(id, CancellationToken.None);
            if (inventory != null)
            {
                return Ok(inventory);
            }
            else
            {
                return NotFound("Inventory not found.");
            }
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

    [HttpGet("GetByName")]
    public async Task<IActionResult> GetByName(string name)
    {
        try
        {
            var inventory = await _inventoryService.GetByName(name, CancellationToken.None);
            if (inventory != null)
            {
                return Ok(inventory);
            }
            else
            {
                return NotFound("Inventory not found.");
            }
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

    [HttpGet("GetAllInventory")]
    public async Task<IActionResult> GetAllInventory()
    {
        try
        {
            var inventories = await _inventoryService.GetAllInventory(CancellationToken.None);
            if (inventories.Count > 0)
            {
                return Ok(inventories);
            }
            else
            {
                return NotFound("No inventory items found.");
            }
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