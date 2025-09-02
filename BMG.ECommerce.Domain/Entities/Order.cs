using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.UnitPrice * i.Quantity);
}