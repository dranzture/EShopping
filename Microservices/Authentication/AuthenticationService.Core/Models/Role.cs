using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Models;

public class Role : IdentityRole<int>, IBaseEntity
{
    public Role(string name, string normalizedName, string? createdBy = null)
    {
        Name = name;
        NormalizedName = normalizedName;
        CreatedBy = createdBy ?? "system";
        CreatedDateTime = DateTime.Today;
    }
    
    public DateTime CreatedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? ModifiedBy { get; set; }
}