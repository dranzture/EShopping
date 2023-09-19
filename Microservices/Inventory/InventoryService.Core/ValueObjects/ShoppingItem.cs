using InventoryService.Core.Dtos;
using InventoryService.Core.Models;

namespace InventoryService.Core.ValueObjects;

public record ShoppingItem
{
    public Inventory Item { get; set; }
    
    public int Amount { get; set;}
    
    public decimal TotalPrice { get; set;}
    
    public DateTimeOffset AddedDateTime { get; set;}
    
    public DateTimeOffset? UpdatedDateTime { get; set;}
}