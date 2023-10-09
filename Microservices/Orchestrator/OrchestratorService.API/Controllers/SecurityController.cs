using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using OrchestratorService.API.Helpers;
using OrchestratorService.Core.Dtos;
using OrchestratorService.Core.Interfaces;

namespace OrchestratorService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SecurityController : ControllerBase
{
    private readonly IGrpcAuthService _service;
    private readonly ILogger<SecurityController> _logger;

    public SecurityController(IGrpcAuthService service, ILogger<SecurityController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost(("Login"))]
    public async Task<IActionResult> Login(LoginRequestDto dto, CancellationToken token)
    {
        try
        {
            return Ok(await _service.Login(dto, token));
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