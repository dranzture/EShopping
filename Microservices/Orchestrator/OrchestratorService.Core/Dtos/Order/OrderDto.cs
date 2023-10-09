using OrchestratorService.Core.Enums;

namespace OrchestratorService.Core.Dtos.Order;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid ShoppingCartId { get; set; }
    public OrderStatus Status { get; set; }
}