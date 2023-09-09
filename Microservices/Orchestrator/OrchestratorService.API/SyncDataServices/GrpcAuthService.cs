using AuthenticationService;
using AuthenticationServiceClient = AuthenticationService.GrpcAuthenticationService.GrpcAuthenticationServiceClient;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using OrchestratorService.Core.Dtos;
using OrchestratorService.Core.Interfaces;
using OrchestratorService.Core.Models;

namespace OrchestratorService.API.SyncDataServices;

public class GrpcAuthService : IGrpcAuthService
{
    private readonly AppSettings _settings;
    private readonly IMapper _mapper;

    public GrpcAuthService(AppSettings settings, IMapper mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }

    public async Task<LoggedUserDto> Login(LoginRequestDto dto, CancellationToken token = default)
    {
        try
        {
            var channel = GrpcChannel.ForAddress(_settings.AuthenticationUrl);
            
            var client = new AuthenticationServiceClient(channel);

            var request = _mapper.Map<LoginUserRequest>(dto);
            var response = await client.LoginUserAsync(request);
            var returnItem = _mapper.Map<LoggedUserDto>(response);
            
            channel.Dispose();
            
            return await Task.FromResult(returnItem);
        }
        catch (RpcException ex)
        {
            if (ex.Status.StatusCode == StatusCode.Unauthenticated)
            {
                throw new UnauthorizedAccessException(ex.Status.Detail);
            }

            return await Task.FromException<LoggedUserDto>(ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Internal Error on Login: {ex.Message}");
            
            return await Task.FromException<LoggedUserDto>(ex);
        }
    }
}