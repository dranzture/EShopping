namespace OrchestratorService.Core.Dtos;

public class LoggedUserDto
{
    public string Email { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string AccessToken { get; set; }
}