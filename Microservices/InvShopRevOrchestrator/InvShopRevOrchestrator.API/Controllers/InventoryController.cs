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
    public IActionResult AddInventory([FromBody] InventoryDto dto)
    {
        try
        {
            var result = _inventoryService.AddInventory(dto, CancellationToken.None).Result;

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
    public IActionResult UpdateInventory([FromBody] InventoryDto dto)
    {
        try
        {
            var username = User.Identity.Name; // Get the username from the authenticated user
            _inventoryService.UpdateInventory(dto, username, CancellationToken.None).Wait();

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
    public IActionResult DecreaseInventory([FromBody] InventoryQuantityChangeBaseDto dto)
    {
        try
        {
            var username = User.Identity.Name; // Get the username from the authenticated user
            _inventoryService.DecreaseInventory(dto, username, CancellationToken.None).Wait();

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
    public IActionResult IncreaseInventory([FromBody] InventoryQuantityChangeBaseDto dto)
    {
        try
        {
            var username = User.Identity.Name; // Get the username from the authenticated user
            _inventoryService.IncreaseInventory(dto, username, CancellationToken.None).Wait();

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
    public IActionResult DeleteInventory([FromBody] InventoryDto dto)
    {
        try
        {
            var username = User.Identity.Name; // Get the username from the authenticated user
            _inventoryService.DeleteInventory(dto, username, CancellationToken.None).Wait();

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
    public IActionResult GetById(Guid id)
    {
        try
        {
            var inventory = _inventoryService.GetById(id, CancellationToken.None).Result;
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
    public IActionResult GetByName(string name)
    {
        try
        {
            var inventory = _inventoryService.GetByName(name, CancellationToken.None).Result;
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
    public IActionResult GetAllInventory()
    {
        try
        {
            var inventories = _inventoryService.GetAllInventory(CancellationToken.None).Result;
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