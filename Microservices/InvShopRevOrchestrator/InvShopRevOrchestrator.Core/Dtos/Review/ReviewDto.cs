namespace InvShopRevOrchestrator.Core.Dtos;

public class ReviewDto : BaseDto<Guid>
{
    public Guid InventoryId { get; set; }
    
    public int UserId { get; set; }
    
    public string Username { get; set; }
    
    public int Stars { get; set; }
    
    public string? Comment { get; set; }
}