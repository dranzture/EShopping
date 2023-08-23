using AuthenticationService;
using AuthenticationServiceClient = AuthenticationService.AuthenticationService.AuthenticationServiceClient;
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
            var call = await client.LoginUserAsync(request);


            return await Task.FromResult(new LoggedUserDto());
        }
        catch(Exception ex)
        {
            return await Task.FromException<LoggedUserDto>(ex);
        }
    }
}