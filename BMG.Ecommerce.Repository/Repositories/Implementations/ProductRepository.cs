using ECommerce.Domain.Entities;
using ECommerce.Repository.Data;
using ECommerce.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BMG.Ecommerce.Repository.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) { _db = db; }
    public async Task<Product> Create(Product p) { _db.Products.Add(p); await _db.SaveChangesAsync(); return p; }
    public Task<Product?> Get(Guid id) => _db.Products.FirstOrDefaultAsync(x => x.Id == id);
    public async Task<IEnumerable<Product>> List() => await _db.Products.AsNoTracking().Where(x => x.Active).ToListAsync();
    public async Task<Product> Update(Guid id, Action<Product> apply) { var p = await _db.Products.FirstAsync(x => x.Id == id); apply(p); await _db.SaveChangesAsync(); return p; }
    public async Task Delete(Guid id) { var p = await _db.Products.FirstAsync(x => x.Id == id); _db.Products.Remove(p); await _db.SaveChangesAsync(); }
    public async Task DecrementStock(Guid id, int qty) { var p = await _db.Products.FirstAsync(x => x.Id == id); if (p.Stock < qty) throw new InvalidOperationException("insufficient-stock"); p.Stock -= qty; await _db.SaveChangesAsync(); }
}