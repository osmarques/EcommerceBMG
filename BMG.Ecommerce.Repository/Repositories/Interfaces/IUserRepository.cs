using ECommerce.Domain.Entities;

namespace ECommerce.Repository.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmail(string email);
    Task<User?> Get(Guid id);
}