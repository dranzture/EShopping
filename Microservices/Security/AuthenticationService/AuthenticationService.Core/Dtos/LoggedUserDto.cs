﻿namespace AuthenticationService.Dtos;

public class LoggedUserDto
{
    public string Email { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Username { get; set; }
    
    public IList<string> Roles { get; set; }
    
    public string AccessToken { get; set; }
}