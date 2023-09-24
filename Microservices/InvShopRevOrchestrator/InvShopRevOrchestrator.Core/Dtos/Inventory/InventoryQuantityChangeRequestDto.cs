namespace InvShopRevOrchestrator.Core.Dtos;

public class InventoryQuantityChangeRequestDto : InventoryQuantityChangeBaseDto
{
    public InventoryQuantityChangeRequestDto(InventoryQuantityChangeBaseDto baseDto, string username) : base(baseDto)
    {
        Username = username;
    }
    public string Username { get; set; }
}