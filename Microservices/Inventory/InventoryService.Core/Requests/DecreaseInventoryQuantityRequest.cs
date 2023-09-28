using InventoryService.Core.Dtos;
using MediatR;

namespace InventoryService.Core.Requests;

public class DecreaseInventoryQuantityRequest : IRequest
{
    public DecreaseInventoryQuantityRequest(ChangeInventoryQuantityDto dto)
    {
        InventoryId = dto.InventoryId;
        Quantity = dto.Quantity;
    }
    
    public Guid InventoryId { get; set; }
    
    public int Quantity { get; set; }
}