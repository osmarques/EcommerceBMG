using System.Security.Cryptography;
using System.Text;
using ECommerce.Business.Dtos;
using ECommerce.Domain.Abstractions;
using ECommerce.Repository.Repositories;

namespace ECommerce.Business.Services;

public class AuthService
{
    private readonly IUserRepository _users;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(IUserRepository users, IJwtTokenGenerator jwt)
    {
        _users = users;
        _jwt = jwt;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _users.GetByEmail(request.Email);
        if (user is null) throw new UnauthorizedAccessException();
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.Password)));
        if (hash != user.PasswordHash) throw new UnauthorizedAccessException();
        var token = _jwt.Generate(user);
        return new LoginResponse(token);
    }
}