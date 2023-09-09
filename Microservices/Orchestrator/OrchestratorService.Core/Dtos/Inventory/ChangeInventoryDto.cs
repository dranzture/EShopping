namespace OrchestratorService.Core.Dtos.Inventory;

public class ChangeInventoryDto
{
    public Guid Id { get; set; }
    
    public int Amount { get; set; }
    
    public string Username { get; set; }
}