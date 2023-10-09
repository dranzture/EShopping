using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchestratorService.API.Helpers;
using OrchestratorService.Core.Dtos.ShippingItem;
using OrchestratorService.Core.Interfaces;

namespace OrchestratorService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ShippingItemController : ControllerBase
{
    private readonly IGrpcShippingItemService _grpcShippingItemService;
    private readonly ILogger<ShippingItemController> _logger;

    public ShippingItemController(IGrpcShippingItemService grpcShippingItemService,
        ILogger<ShippingItemController> logger)
    {
        _grpcShippingItemService = grpcShippingItemService;
        _logger = logger;
    }


    [HttpPut("UpdateShippingItem")]
    public async Task<IActionResult> UpdateShippingItemStatus([FromBody] UpdateShippingStatusDto dto)
    {
        try
        {
            await _grpcShippingItemService.UpdateShippingItemStatus(dto);
            _logger.LogInformation("ShippingItem updated successfully.");
            return Ok("ShippingItem updated successfully.");
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


    [HttpGet("GetShippingItemById")]
    public async Task<IActionResult> GetShippingItemById(Guid id)
    {
        try
        {
            var result = await _grpcShippingItemService.GetShippingItemById(id);
            if (result != null)
            {
                _logger.LogInformation("ShippingItem retrieved successfully.");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("ShippingItem not found.");
                return NotFound();
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

    [HttpGet("GetShippingItemByOrderId")]
    public async Task<IActionResult> GetShippingItemByOrderId(Guid orderId)
    {
        try
        {
            var result = await _grpcShippingItemService.GetShippingItemByOrderId(orderId);
            if (result != null)
            {
                _logger.LogInformation("ShippingItem retrieved successfully.");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("ShippingItem not found.");
                return NotFound();
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