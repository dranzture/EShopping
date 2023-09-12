using AuthenticationService.Core.Interfaces;
using AuthenticationService.Dtos;
using AutoMapper;
using Grpc.Core;
using GrpcAuthenticationService;

namespace AuthenticationService.SyncDataServices;

public class GrpcService : GrpcAuthenticationService.GrpcAuthenticationService.GrpcAuthenticationServiceBase
{
    private readonly ILoggingUserService _authService;
    private readonly IMapper _mapper;

    public GrpcService(ILoggingUserService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public override async Task<LoggedUserResponse> LoginUser(LoginUserRequest request, ServerCallContext context)
    {
        var dto = _mapper.Map<LoginRequestDto>(request);
        
        var result = await _authService.LoginUser(dto);
        
        return _mapper.Map<LoggedUserResponse>(result);
    }
}