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
                logger.LogWarning(rpcEx.Status.Detail);
                return controller.NotFound(rpcEx.Status.Detail);
            case StatusCode.InvalidArgument:
                logger.LogWarning(rpcEx.Status.Detail);
                return controller.BadRequest(rpcEx.Status.Detail);
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