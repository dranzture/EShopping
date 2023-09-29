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
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
    {
        _reviewService = reviewService;
        _logger = logger;
    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
    {
        try
        {
            var reviewId =
                await _reviewService.AddReview(dto, HttpContext!.User!.Identity!.Name!, CancellationToken.None);

            return Ok(new CreatedReviewResultDto
            {
                Id = reviewId,
                Message = "Review added successfully"
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

    [HttpPut("UpdateReview")]
    public async Task<IActionResult> UpdateReview([FromBody] ReviewDto dto)
    {
        try
        {
            await _reviewService.UpdateReview(dto, HttpContext.User.Identity.Name, CancellationToken.None);

            _logger.LogInformation("Review updated successfully");
            return Ok("Review updated successfully.");
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

    [HttpDelete("DeleteReview")]
    public async Task<IActionResult> DeleteReview([FromQuery] Guid id)
    {
        try
        {
            await _reviewService.DeleteReview(id, CancellationToken.None);

            _logger.LogInformation("Review deleted successfully");
            return Ok("Review deleted successfully.");
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

    [HttpGet("GetReviewsByInventoryId")]
    public async Task<IActionResult> GetReviewsByInventoryId(Guid id)
    {
        try
        {
            var reviews = await _reviewService.GetReviewsByInventoryId(id, CancellationToken.None);
            return Ok(reviews);
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

    [HttpGet("GetReviewsByUsername")]
    public async Task<IActionResult> GetReviewsByUsername()
    {
        try
        {
            var reviews =
                await _reviewService.GetReviewsByUsername(HttpContext!.User!.Identity!.Name!, CancellationToken.None);
            return Ok(reviews);
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

    [HttpGet("GetReviewByInventoryIdAndUsername")]
    public async Task<IActionResult> GetReviewByInventoryIdAndUsername(Guid id)
    {
        try
        {
            var review =
                await _reviewService.GetReviewByInventoryIdAndUsername(id, HttpContext!.User!.Identity!.Name!,
                    CancellationToken.None);
            return Ok(review);
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

    [HttpGet("GetReviewById")]
    public async Task<IActionResult> GetReviewById(Guid id)
    {
        try
        {
            var review = await _reviewService.GetReviewById(id, CancellationToken.None);
            return Ok(review);
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