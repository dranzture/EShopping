namespace InvShopRevOrchestrator.Core.Dtos;

public class InventoryListItemDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int InStock { get; set; }
    public float Price { get; set; }
}