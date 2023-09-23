using Grpc.Core;
using InvShopRevOrchestrator.Core.Dtos.Inventory;
using InvShopRevOrchestrator.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvShopRevOrchestrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> AddInventory([FromBody] UpdateInventoryDto dto)
        {
            try
            {

                return Ok(new CreatedInventoryResultDto()
                {
                    //Id = result,
                    Message = "Inventory added successfully"
                });
            }
            catch (RpcException rpcEx)
            {
                switch (rpcEx.StatusCode)
                {
                    case Grpc.Core.StatusCode.NotFound:
                        _logger.LogWarning("Inventory not found");
                        return NotFound("Inventory not found.");
                    case Grpc.Core.StatusCode.InvalidArgument:
                        _logger.LogWarning("Invalid argument provided");
                        return BadRequest("Invalid argument provided.");
                    default:
                        _logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                        return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: {ExMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("DecreaseInventory")]
        public async Task<IActionResult> DecreaseInventory([FromBody] UpdateInventoryDto dto)
        {
            try
            {
                _logger.LogInformation("Inventory decreased successfully");
                return Ok("Inventory decreased successfully.");
            }
            catch (RpcException rpcEx)
            {
                switch (rpcEx.StatusCode)
                {
                    case Grpc.Core.StatusCode.NotFound:
                        _logger.LogWarning("Inventory not found");
                        return NotFound("Inventory not found.");
                    case Grpc.Core.StatusCode.InvalidArgument:
                        _logger.LogWarning("Invalid argument provided");
                        return BadRequest("Invalid argument provided.");
                    default:
                        _logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                        return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: {ExMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("IncreaseInventory")]
        public async Task<IActionResult> IncreaseInventory([FromBody] UpdateInventoryDto dto)
        {
            try
            {
                //await _grpcInventoryService.IncreaseInventory(dto);
                _logger.LogInformation("Inventory increased successfully");
                return Ok("Inventory increased successfully.");
            }
            catch (RpcException rpcEx)
            {
                switch (rpcEx.StatusCode)
                {
                    case Grpc.Core.StatusCode.NotFound:
                        _logger.LogWarning("Inventory not found");
                        return NotFound("Inventory not found.");
                    case Grpc.Core.StatusCode.InvalidArgument:
                        _logger.LogWarning("Invalid argument provided");
                        return BadRequest("Invalid argument provided.");
                    default:
                        _logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                        return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: {ExMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteInventory")]
        public async Task<IActionResult> DeleteInventory([FromQuery] Guid id)
        {
            try
            {
                _logger.LogInformation("Inventory deleted successfully");
                return Ok("Inventory deleted successfully.");
            }
            catch (RpcException rpcEx)
            {
                switch (rpcEx.StatusCode)
                {
                    case Grpc.Core.StatusCode.NotFound:
                        _logger.LogWarning("Inventory not found");
                        return NotFound("Inventory not found.");
                    case Grpc.Core.StatusCode.InvalidArgument:
                        _logger.LogWarning("Invalid argument provided");
                        return BadRequest("Invalid argument provided.");
                    default:
                        _logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                        return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: {ExMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateInventory")]
        public async Task<IActionResult> UpdateInventory([FromBody] UpdateInventoryDto dto)
        {
            try
            {
                _logger.LogInformation("Inventory updated successfully");
                return Ok("Inventory updated successfully.");
            }
            catch (RpcException rpcEx)
            {
                switch (rpcEx.StatusCode)
                {
                    case Grpc.Core.StatusCode.NotFound:
                        _logger.LogWarning("Inventory not found");
                        return NotFound("Inventory not found.");
                    case Grpc.Core.StatusCode.InvalidArgument:
                        _logger.LogWarning("Invalid argument provided");
                        return BadRequest("Invalid argument provided.");
                    default:
                        _logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                        return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: {ExMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok();   
            }
            catch (RpcException rpcEx)
            {
                switch (rpcEx.StatusCode)
                {
                    case Grpc.Core.StatusCode.NotFound:
                        _logger.LogWarning("Inventory not found");
                        return NotFound("Inventory not found.");
                    case Grpc.Core.StatusCode.InvalidArgument:
                        _logger.LogWarning("Invalid argument provided");
                        return BadRequest("Invalid argument provided.");
                    default:
                        _logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                        return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: {ExMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                return Ok();
            }
            catch (RpcException rpcEx)
            {
                switch (rpcEx.StatusCode)
                {
                    case Grpc.Core.StatusCode.NotFound:
                        _logger.LogWarning("Inventory not found");
                        return NotFound("Inventory not found.");
                    case Grpc.Core.StatusCode.InvalidArgument:
                        _logger.LogWarning("Invalid argument provided");
                        return BadRequest("Invalid argument provided.");
                    default:
                    {
                        if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                        {
                            return NotFound();
                        }
                        else
                        {
                            _logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                            return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error: {ExMessage}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}