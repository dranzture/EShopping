namespace OrchestratorService.Core.Dtos.Inventory;

public class InventoryDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int InStock { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Weight { get; set; }
}