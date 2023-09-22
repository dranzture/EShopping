using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Core.Models;

public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTimeOffset CreatedDateTime { get; protected set; }
    
    [Required]
    public string CreatedBy { get; protected set; }
    
    public DateTimeOffset? ModifiedDateTime { get; protected set; }
    
    public string? ModifiedBy { get; protected set; }
    
    public bool IsDeleted { get; protected set; }
    
    public DateTimeOffset? DeletedDateTime { get; protected set; }
    
    public string? DeletedBy { get; protected set; }
    
    protected void UpdateModifiedFields(string username)
    {
        ModifiedDateTime = DateTimeOffset.Now;
        ModifiedBy = username;
    }
    
    protected void UpdateDeletedFields(string username)
    {
        DeletedDateTime = DateTimeOffset.Now;
        DeletedBy = username;
    }
}