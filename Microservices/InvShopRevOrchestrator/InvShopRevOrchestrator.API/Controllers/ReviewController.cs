using Grpc.Core;
using InvShopRevOrchestrator.Core.Dtos.Review;
using InvShopRevOrchestrator.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvShopRevOrchestrator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IGrpcReviewService _grpcReviewService;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(IGrpcReviewService grpcReviewService, ILogger<ReviewController> logger)
        {
            _grpcReviewService = grpcReviewService;
            _logger = logger;
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
        {
            try
            {
                var result = await _grpcReviewService.AddReview(dto);
                _logger.LogInformation($"Review added successfully with Id: {result.ToString()}.", result);

                return Ok(new CreatedReviewResultDto()
                {
                    Id = result,
                    Message = "Review added successfully"
                });
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Review not found.");
                    return NotFound("Review not found.");
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

        [HttpPut("UpdateReview")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewDto dto)
        {
            try
            {
                await _grpcReviewService.UpdateReview(dto);
                _logger.LogInformation("Review updated successfully.");
                return Ok("Review updated successfully.");
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Review not found.");
                    return NotFound("Review not found.");
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
        
        [HttpGet("GetReviewsByInventoryId")]
        public async Task<IActionResult> GetReviewsByInventoryId(Guid id)
        {
            try
            {
                var result = await _grpcReviewService.GetReviewsByInventoryId(id);
                if (result != null)
                {
                    _logger.LogInformation("Reviews retrieved successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Reviews not found.");
                    return NotFound();
                }
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Reviews not found.");
                    return NotFound("Reviews not found.");
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

        [HttpGet("GetReviewsByUserId")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            try
            {
                var result = await _grpcReviewService.GetReviewsByUserId(userId);
                if (result != null)
                {
                    _logger.LogInformation("Reviews retrieved successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Reviews not found.");
                    return NotFound();
                }
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Reviews not found.");
                    return NotFound("Reviews not found.");
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

        [HttpGet("GetReviewByUserIdAndInventoryId")]
        public async Task<IActionResult> GetReviewByUserIdAndInventoryId(Guid id, int userId)
        {
            try
            {
                var result = await _grpcReviewService.GetReviewByUserIdAndInventoryId(id, userId);
                if (result != null)
                {
                    _logger.LogInformation("Review retrieved successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Review not found.");
                    return NotFound();
                }
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Review not found.");
                    return NotFound("Review not found.");
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

        [HttpGet("GetReviewById")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            try
            {
                var result = await _grpcReviewService.GetReviewById(id);
                if (result != null)
                {
                    _logger.LogInformation("Review retrieved successfully.");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Review not found.");
                    return NotFound();
                }
            }
            catch (RpcException rpcEx)
            {
                if (rpcEx.StatusCode == Grpc.Core.StatusCode.NotFound)
                {
                    _logger.LogWarning("Review not found.");
                    return NotFound("Review not found.");
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