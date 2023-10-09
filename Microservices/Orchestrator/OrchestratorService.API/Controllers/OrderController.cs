using Microsoft.AspNetCore.Mvc;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using OrchestratorService.API.Helpers;
using OrchestratorService.Core.Interfaces;
using OrderService.Core.Dtos;

namespace OrchestratorService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IGrpcOrderService _grpcOrderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IGrpcOrderService grpcOrderService, ILogger<OrderController> logger)
    {
        _grpcOrderService = grpcOrderService;
        _logger = logger;
    }

    [HttpPut("ReprocessOrder")]
    public async Task<IActionResult> ReprocessOrder([FromBody] ReprocessOrderDto dto)
    {
        try
        {
            await _grpcOrderService.ReprocessOrderById(dto.Id);
            _logger.LogInformation("Order reprocessed successfully.");
            return Ok();
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

    [HttpGet("GetOrdersByUsername")]
    public async Task<IActionResult> GetOrdersByUsername()
    {
        try
        {
            var result = await _grpcOrderService
                .GetOrdersByUsername(HttpContext.User.Identity.Name);
            if (result != null)
            {
                _logger.LogInformation("Orders retrieved successfully.");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Order not found.");
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

    [HttpGet("GetByOrderId")]
    public async Task<IActionResult> GetByOrderId(Guid id)
    {
        try
        {
            var result = await _grpcOrderService.GetByOrderId(id);
            if (result != null)
            {
                _logger.LogInformation("Order retrieved successfully.");
                return Ok(result);
            }
            else
            {
                _logger.LogWarning("Order not found.");
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