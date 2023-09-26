namespace InvShopRevOrchestrator.Core.Dtos;

public class InventoryQuantityChangeRequestDto
{
    public InventoryQuantityChangeRequestDto(InventoryDto dto, int amount, string username)
    {
        Dto = dto;
        Amount = amount;
        Username = username;
        
    }
    public InventoryDto Dto { get; set; }

    public int Amount { get; set; }
    
    public string Username { get; set; }
}