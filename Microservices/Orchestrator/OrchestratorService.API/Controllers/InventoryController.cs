using Microsoft.AspNetCore.Mvc;
using Grpc.Core;
using OrchestratorService.Core.Dtos.Inventory;
using OrchestratorService.Core.Interfaces;

namespace OrchestratorService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IGrpcInventoryService _grpcInventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IGrpcInventoryService grpcInventoryService, ILogger<InventoryController> logger)
        {
            _grpcInventoryService = grpcInventoryService;
            _logger = logger;
        }

        [HttpPost("AddInventory")]
        public async Task<IActionResult> AddInventory([FromBody] MutateInventoryDto dto)
        {
            try
            {
                var result = await _grpcInventoryService.AddInventory(dto);
                _logger.LogInformation($"Inventory added successfully with Id:{result.ToString()}.", result);

                return Ok(new CreatedInventoryResultDto()
                {
                    Id = result,
                    Message = "Inventory added successfully"
                });
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound("Inventory not found.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.InvalidArgument)
                {
                    _logger.LogWarning("Invalid argument provided.");
                    return BadRequest("Invalid argument provided.");
                }
                else
                {
                    _logger.LogError($"gRPC error: {rpcEx.Status.Detail}");
                    return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("DecreaseInventory")]
        public async Task<IActionResult> DecreaseInventory([FromBody] ChangeInventoryDto dto)
        {
            try
            {
                await _grpcInventoryService.DecreaseInventory(dto);
                _logger.LogInformation("Inventory decreased successfully.");
                return Ok("Inventory decreased successfully.");
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound("Inventory not found.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.InvalidArgument)
                {
                    _logger.LogWarning("Invalid argument provided.");
                    return BadRequest("Invalid argument provided.");
                }
                else
                {
                    _logger.LogError($"gRPC error: {rpcEx.Status.Detail}");
                    return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("IncreaseInventory")]
        public async Task<IActionResult> IncreaseInventory([FromBody] ChangeInventoryDto dto)
        {
            try
            {
                await _grpcInventoryService.IncreaseInventory(dto);
                _logger.LogInformation("Inventory increased successfully.");
                return Ok("Inventory increased successfully.");
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound("Inventory not found.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.InvalidArgument)
                {
                    _logger.LogWarning("Invalid argument provided.");
                    return BadRequest("Invalid argument provided.");
                }
                else
                {
                    _logger.LogError($"gRPC error: {rpcEx.Status.Detail}");
                    return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteInventory")]
        public async Task<IActionResult> DeleteInventory([FromQuery] Guid id)
        {
            try
            {
                await _grpcInventoryService.DeleteInventory(id);
                _logger.LogInformation("Inventory deleted successfully.");
                return Ok("Inventory deleted successfully.");
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound("Inventory not found.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.InvalidArgument)
                {
                    _logger.LogWarning("Invalid argument provided.");
                    return BadRequest("Invalid argument provided.");
                }
                else
                {
                    _logger.LogError($"gRPC error: {rpcEx.Status.Detail}");
                    return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateInventory")]
        public async Task<IActionResult> UpdateInventory([FromBody] MutateInventoryDto dto)
        {
            try
            {
                await _grpcInventoryService.UpdateInventory(dto);
                _logger.LogInformation("Inventory updated successfully.");
                return Ok("Inventory updated successfully.");
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound("Inventory not found.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.InvalidArgument)
                {
                    _logger.LogWarning("Invalid argument provided.");
                    return BadRequest("Invalid argument provided.");
                }
                else
                {
                    _logger.LogError($"gRPC error: {rpcEx.Status.Detail}");
                    return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _grpcInventoryService.GetById(id);
                if (result != null)
                {
                    _logger.LogInformation("Inventory retrieved successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound();
                }
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound("Inventory not found.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.InvalidArgument)
                {
                    _logger.LogWarning("Invalid argument provided.");
                    return BadRequest("Invalid argument provided.");
                }
                else
                {
                    _logger.LogError($"gRPC error: {rpcEx.Status.Detail}");
                    return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var result = await _grpcInventoryService.GetByName(name);
                if (result != null)
                {
                    _logger.LogInformation("Inventory retrieved successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound();
                }
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Inventory not found.");
                    return NotFound("Inventory not found.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.InvalidArgument)
                {
                    _logger.LogWarning("Invalid argument provided.");
                    return BadRequest("Invalid argument provided.");
                }
                else if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"gRPC error: {rpcEx.Status.Detail}");
                    return StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}