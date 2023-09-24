namespace InvShopRevOrchestrator.Core.Dtos;

public class UserAndInventoryIdParam
{
    public int UserId { get; set; }
    
    public Guid InventoryId { get; set; }
}