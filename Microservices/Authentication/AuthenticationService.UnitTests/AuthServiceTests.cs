using AuthenticationService.Core.Interfaces;
using AuthenticationService.Core.Services;
using AuthenticationService.Dtos;
using AuthenticationService.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using NSubstitute;


namespace AuthenticationService.UnitTests;

public class LoggingUserServiceTests
{
    private readonly ILoggingUserService _service;
    private readonly List<string> _roleList = new List<string>() { "Owner", "Admin", "Customer" };
    public LoggingUserServiceTests()
    {
        var store = Substitute.For<IUserStore<User>>();
        var manager = Substitute.For<UserManager<User>>(store,null,null,null,null,null,null,null,null);
        var jwt = Substitute.For<IJwtService>();
        var user = new User
        {
            Email = "polatcoban@gmail.com",
            FirstName = "Polat",
            LastName = "Coban"
        };
        var tokenResult = "GeneratedToken!";
        manager.FindByNameAsync("polatcoban@gmail.com").Returns(Task.FromResult(user));
        manager.FindByEmailAsync("polatcoban@gmail.com").Returns(Task.FromResult(user));
        manager.CheckPasswordAsync(user, "qaz123").Returns(Task.FromResult(true));
        manager.GetRolesAsync(user).Returns(Task.FromResult((IList<string>)_roleList));
        jwt.GenerateJwtToken(user, _roleList).Returns(Task.FromResult(tokenResult));

        _service = new LoggingUserService(manager, jwt);
    }

    [Fact]
    public async void LoginUser_ShouldReturnUserResponse_WhenItIsCorrect()
    {
        //Arrange

        var desiredResult = new LoggedUserDto()
        {
            AccessToken = "GeneratedToken!",
            Email = "polatcoban@gmail.com",
            FirstName = "Polat",
            LastName = "Coban"
        };
        var loginRequest = new LoginRequestDto()
        {
            Password = "qaz123",
            Username = "polatcoban@gmail.com"
        };

        //Act
        var result = await _service.LoginUser(loginRequest);

        //Assert
        Assert.Equal(desiredResult.FirstName, result.FirstName);
        Assert.Equal(desiredResult.LastName, result.LastName);
        Assert.Equal(desiredResult.Email, result.Email);
        Assert.Equal(desiredResult.AccessToken, result.AccessToken);
        Assert.Equal(_roleList, result.Roles);
    }

    [Fact]
    public async void LoginUser_ShouldThrowUnauthorizedException_WhenWrongPassword()
    {
        //Arrange

        var desiredResult = new LoggedUserDto()
        {
            AccessToken = "GeneratedToken",
            Email = "polatcoban@gmail.com",
            FirstName = "Polat",
            LastName = "Coban"
        };
        var loginRequest = new LoginRequestDto()
        {
            Password = "qaz1234",
            Username = "polatcoban@gmail.com"
        };

        //Act & Assert
        await Assert.ThrowsAsync<RpcException>(() => _service.LoginUser(loginRequest));
    }
    
    [Fact]
    public async void LoginUser_ShouldThrowUnauthorizedException_WhenUserNotFound()
    {
        //Arrange

        var desiredResult = new LoggedUserDto()
        {
            AccessToken = "GeneratedToken",
            Email = "polatcoban@gmail.com",
            FirstName = "Polat",
            LastName = "Coban"
        };
        var loginRequest = new LoginRequestDto()
        {
            Password = "qaz123",
            Username = "polatcoban2@gmail.com"
        };

        //Act & Assert
        await Assert.ThrowsAsync<RpcException>(() => _service.LoginUser(loginRequest));
    }
}