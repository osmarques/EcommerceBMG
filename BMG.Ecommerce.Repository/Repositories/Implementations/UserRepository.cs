using ECommerce.Domain.Entities;
using ECommerce.Repository.Data;
using ECommerce.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BMG.Ecommerce.Repository.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) { _db = db; }
    public Task<User?> GetByEmail(string email) => _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    public Task<User?> Get(Guid id) => _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}