using ECommerce.Business.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Repositories;

namespace ECommerce.Business.Services;

public class CartService
{
    private readonly IProductRepository _products;
    private readonly IOrderRepository _orders;

    public CartService(IProductRepository products, IOrderRepository orders)
    {
        _products = products;
        _orders = orders;
    }

    public async Task<Order> AddItem(Guid userId, AddToCartRequest req)
    {
        var order = await _orders.GetActiveCart(userId) ?? await _orders.CreateCart(userId);
        var product = await _products.Get(req.ProductId) ?? throw new InvalidOperationException("product-not-found");
        if (req.Quantity <= 0) throw new InvalidOperationException("invalid-quantity");
        if (product.Stock < req.Quantity) throw new InvalidOperationException("insufficient-stock");
        await _orders.AddItem(order.Id, product.Id, req.Quantity, product.Price);
        await _products.DecrementStock(product.Id, req.Quantity);
        return await _orders.Get(order.Id) ?? throw new InvalidOperationException("order-not-found");
    }
}