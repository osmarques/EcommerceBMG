using ECommerce.Domain.Entities;

namespace ECommerce.Repository.Repositories;

public interface IProductRepository
{
    Task<Product> Create(Product p);
    Task<Product?> Get(Guid id);
    Task<IEnumerable<Product>> List();
    Task<Product> Update(Guid id, Action<Product> apply);
    Task Delete(Guid id);
    Task DecrementStock(Guid id, int qty);
}   