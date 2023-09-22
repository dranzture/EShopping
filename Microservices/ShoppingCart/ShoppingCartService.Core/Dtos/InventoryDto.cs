namespace ShoppingCartService.Core.Dtos;

public class InventoryDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public int InStock { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Weight { get; set; }
    
    public decimal Price { get; set; }
}