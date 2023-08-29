using OrchestratorService.Core.Dtos;

namespace OrchestratorService.Core.Interfaces;

public interface IGrpcAuthService
{
    Task<LoggedUserDto> Login(LoginRequestDto dto, CancellationToken token = default);
}