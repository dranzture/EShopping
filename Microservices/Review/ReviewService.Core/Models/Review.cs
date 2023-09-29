using System.ComponentModel.DataAnnotations;
using ReviewService.Core.Interfaces;

namespace ReviewService.Core.Models;

public class Review : BaseEntity
{
    public Review(){}//For Ef
    
    public Review(Guid inventoryId,  string username, int stars, string? comment = null)
    {
        InventoryId = inventoryId;
        Username = CreatedBy = username;
        Stars = stars;
        Comment = comment;
        CreatedDateTime = DateTimeOffset.Now;
    }
    [Required]
    public Guid InventoryId { get; private set; }
    
    
    [Required]
    public string Username { get; private set; }
    
    [Required]
    public int Stars { get; private set; }
    
    public string? Comment { get; private set; }
    
    public void UpdateStars(int stars, string username)
    {
        Stars = stars;
        UpdateModifiedFields(username);
    }
    
    public void UpdateComment(string comment, string username)
    {
        Comment = comment;
        UpdateModifiedFields(username);
    }
}