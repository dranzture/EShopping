namespace ReviewService.Core.Dtos;

public class ReviewDto
{
    public Guid? Id { get; set; }
    
    public Guid InventoryId { get; set; }
    
    public int ExternalUserId { get; set; }
    
    public string Username { get; set; }
    
    public int Stars { get; set; }
    
    public string? Comment { get; set; }
}