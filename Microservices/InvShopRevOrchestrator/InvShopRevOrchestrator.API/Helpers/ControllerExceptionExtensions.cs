using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
namespace InvShopRevOrchestrator.API.Helpers;

public static class ControllerExceptionExtensions
{
    public static IActionResult HandleRpcException(this ControllerBase controller, RpcException rpcEx, ILogger logger)
    {
        switch (rpcEx.StatusCode)
        {
            case StatusCode.NotFound:
                logger.LogWarning("Resource not found");
                return controller.NotFound("Resource not found.");
            case StatusCode.InvalidArgument:
                logger.LogWarning("Invalid argument provided");
                return controller.BadRequest("Invalid argument provided.");
            default:
                logger.LogError("gRPC error: {StatusDetail}", rpcEx.Status.Detail);
                return controller.StatusCode(500, $"gRPC error: {rpcEx.Status.Detail}");
        }
    }

    public static IActionResult HandleException(this ControllerBase controller, Exception ex, ILogger logger)
    {
        logger.LogError("Internal server error: {ExMessage}", ex.Message);
        return controller.StatusCode(500, $"Internal server error: {ex.Message}");
    }
}