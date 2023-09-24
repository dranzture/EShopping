namespace InvShopRevOrchestrator.Core.Dtos;

public abstract class InventoryQuantityChangeBaseDto
{
    protected InventoryQuantityChangeBaseDto(InventoryQuantityChangeBaseDto initializer)
    {
        Dto = initializer.Dto;
        Amount = initializer.Amount;
    }
    public InventoryDto Dto { get; set; }
    
    public int Amount { get; set; }
}