using AuthenticationService;
using AuthenticationServiceClient = AuthenticationService.GrpcAuthenticationService.GrpcAuthenticationServiceClient;
using AutoMapper;
using Grpc.Net.Client;
using OrchestratorService.Core.Dtos;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;

namespace OrchestratorService.API.SyncDataServices;

public class GrpcService : IGrpcService
{
    private readonly AppSettings _settings;
    private readonly GrpcChannel _channel;
    private readonly IMapper _mapper;

    public GrpcService(AppSettings settings, IMapper mapper)
    {
        _settings = settings;
        _channel = GrpcChannel.ForAddress(_settings.AuthenticationUrl);
        _mapper = mapper;
    }

    public async Task<LoggedUserDto> Login(LoginRequestDto dto, CancellationToken token = default)
    {
        try
        {
            var client = new AuthenticationServiceClient(_channel);

            var request = _mapper.Map<LoginUserRequest>(dto);
            var response = await client.LoginUserAsync(request);
            var returnItem = _mapper.Map<LoggedUserDto>(response);

            return await Task.FromResult(returnItem);
        }
        catch(Exception ex)
        {
            return await Task.FromException<LoggedUserDto>(ex);
        }
    }
}