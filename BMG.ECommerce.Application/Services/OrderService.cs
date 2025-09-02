using ECommerce.Business.Dtos;
using ECommerce.Domain.Enums;
using ECommerce.Repository.Repositories;

namespace ECommerce.Business.Services;

public class OrderService
{
    private readonly IOrderRepository _orders;

    public OrderService(IOrderRepository orders)
    {
        _orders = orders;
    }

    public async Task<OrderResponse> Checkout(Guid userId, CheckoutRequest req)
    {
        var order = await _orders.GetActiveCart(userId) ?? throw new InvalidOperationException("cart-not-found");
        if (order.Items.Count == 0) throw new InvalidOperationException("empty-cart");
        if (req.Method != "pix" && req.Method != "card") throw new InvalidOperationException("invalid-method");
        await _orders.MarkAsPaid(order.Id);
        var paid = await _orders.Get(order.Id) ?? throw new InvalidOperationException("order-not-found");
        return new OrderResponse(paid.Id, paid.Total, paid.Status.ToString());
    }

    public Task<IEnumerable<(Guid Id, decimal Total, string Status, DateTime CreatedAt)>> History(Guid userId) => _orders.History(userId);
}