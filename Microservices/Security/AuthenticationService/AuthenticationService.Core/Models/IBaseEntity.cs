namespace AuthenticationService.Models;

public interface IBaseEntity
{
   public DateTime CreatedDateTime { get; set; }
   
   public string CreatedBy { get; set; }
   
   public DateTime? ModifiedDateTime { get; set; }
   
   public string? ModifiedBy { get; set; }
}