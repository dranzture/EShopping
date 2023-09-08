using Microsoft.AspNetCore.Mvc;
using OrchestratorService.Core.Dtos;
using OrchestratorService.Core.Interfaces;

namespace OrchestratorService.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SecurityController : ControllerBase
{
    private readonly IGrpcAuthService _service;

    public SecurityController(IGrpcAuthService service)
    {
        _service = service;
    }

    [HttpPost(("Login"))]
    public async Task<ActionResult<LoggedUserDto>> Login(LoginRequestDto dto, CancellationToken token)
    {
        try
        {
            return Ok(await _service.Login(dto, token));
        }
        catch
        {
            return BadRequest();
        }
    }
}