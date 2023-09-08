namespace OrchestratorService.Core.Dtos.Review;

public class UserAndInventoryIdParam
{
    public int UserId { get; set; }
    
    public Guid InventoryId { get; set; }
}