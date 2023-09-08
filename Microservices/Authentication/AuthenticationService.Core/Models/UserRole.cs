using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Models;

public class UserRole : IdentityUserRole<int>, IBaseEntity
{
    public DateTime CreatedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? ModifiedBy { get; set; }
}