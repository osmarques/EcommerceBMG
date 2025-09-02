using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Abstractions;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}