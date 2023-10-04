using System;
using System.ComponentModel.DataAnnotations.Schema;
using OrderService.Core.Helpers;

namespace OrderService.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    public DateTimeOffset CreatedDateTime { get; protected set; }
    
    public string CreatedBy { get; protected set; }
    

    private List<DomainEventBase> _domainEvents = new();
    
    [NotMapped] public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    
    public void ClearDomainEvents() => _domainEvents.Clear();
}