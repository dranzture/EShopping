using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace ShoppingCartService.Core.Entities;

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
    
    protected void UpdateCreatedFields(string username)
    {
        CreatedDateTime = DateTimeOffset.Now;
        CreatedBy = username;
    }
    
    protected internal void UpdateModifiedFields(string username)
    {
        ModifiedDateTime = DateTimeOffset.Now;
        ModifiedBy = username;
    }
    
    protected internal void UpdateDeletedFields(string username)
    {
        DeletedDateTime = DateTimeOffset.Now;
        DeletedBy = username;
        IsDeleted = true;
    }
    private List<INotification> _domainEvents;
    
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}