using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Repository.Repositories;

public interface IOrderRepository
{
    Task<Order?> Get(Guid id);
    Task<Order?> GetActiveCart(Guid userId);
    Task<Order> CreateCart(Guid userId);
    Task AddItem(Guid orderId, Guid productId, int quantity, decimal unitPrice);
    Task MarkAsPaid(Guid orderId);
    Task<IEnumerable<(Guid Id, decimal Total, string Status, DateTime CreatedAt)>> History(Guid userId);
}