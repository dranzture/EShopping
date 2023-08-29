using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Models;

public class User : IdentityUser<int>, IBaseEntity
{
    public User(string fname, string lname, string email, string? createdBy = null)
    {
        FirstName = fname;
        LastName = lname;
        Email = email;
        UserName = email;
        IsDeleted = false;
        SecurityStamp = Guid.NewGuid().ToString("D");
        CreatedBy = createdBy ?? "system";
        CreatedDateTime = DateTime.Now;
    }
    
    public User(){}
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? ModifiedBy { get; set; }
    
    public ICollection<Role> UserRoles { get; set; }
}