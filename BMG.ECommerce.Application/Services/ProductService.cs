using BMG.ECommerce.Business.Dtos;
using ECommerce.Business.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Repositories;

namespace ECommerce.Business.Services;

public class ProductService
{
    private readonly IProductRepository _products;

    public ProductService(IProductRepository products)
    {
        _products = products;
    }

    public async Task<ProductResponse> Create(ProductCreateRequest r)
    {
        var product = new Product
        {
            Name = r.Name,
            Description = r.Description,
            Price = r.Price,
            Stock = r.Stock,
            Active = true
        };

        await _products.Create(product);

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public async Task<ProductResponse?> Get(Guid id)
    {
        var product = await _products.Get(id);
        if (product == null) return null;
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public async Task<IEnumerable<ProductResponse>> List()
    {
        var products = await _products.List();
        return products.Select(p => new ProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock
        });
    }

    public async Task<ProductResponse> Update(Guid id, ProductUpdateRequest r)
    {
        var product = await _products.Update(id, p =>
        {
            p.Name = r.Name;
            p.Description = r.Description;
            p.Price = r.Price;
            p.Stock = r.Stock;
            p.Active = r.Active;
        });

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public Task Delete(Guid id) => _products.Delete(id);
}
