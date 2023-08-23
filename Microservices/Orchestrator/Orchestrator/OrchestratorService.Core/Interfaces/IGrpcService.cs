using OrchestratorService.Core.Dtos;

namespace OrchestratorService.Core.Interfaces;

public interface IGrpcService
{
    Task<LoggedUserDto> Login(LoginRequestDto dto, CancellationToken token = default);
}