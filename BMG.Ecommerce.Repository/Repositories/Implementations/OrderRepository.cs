using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Repository.Data;
using ECommerce.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BMG.Ecommerce.Repository.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;
    public OrderRepository(AppDbContext db) { _db = db; }

    public Task<Order?> Get(Guid id) => _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(x => x.Id == id);

    public Task<Order?> GetActiveCart(Guid userId) => _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(x => x.UserId == userId && x.Status == OrderStatus.Pending);

    public async Task<Order> CreateCart(Guid userId)
    {
        var o = new Order { Id = Guid.NewGuid(), UserId = userId, Status = OrderStatus.Pending };
        _db.Orders.Add(o);
        await _db.SaveChangesAsync();
        return o;
    }

    public async Task AddItem(Guid orderId, Guid productId, int quantity, decimal unitPrice)
    {
        var existing = await _db.OrderItems.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        if (existing is null) _db.OrderItems.Add(new OrderItem { Id = Guid.NewGuid(), OrderId = orderId, ProductId = productId, Quantity = quantity, UnitPrice = unitPrice });
        else existing.Quantity += quantity;
        await _db.SaveChangesAsync();
    }

    public async Task MarkAsPaid(Guid orderId)
    {
        var o = await _db.Orders.FirstAsync(x => x.Id == orderId);
        o.Status = OrderStatus.Paid;
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<(Guid Id, decimal Total, string Status, DateTime CreatedAt)>> History(Guid userId)
    {
        var list = await _db.Orders.Include(o => o.Items).Where(o => o.UserId == userId).OrderByDescending(o => o.CreatedAt).ToListAsync();
        return list.Select(o => (o.Id, o.Items.Sum(i => i.UnitPrice * i.Quantity), o.Status.ToString(), o.CreatedAt));
    }
}