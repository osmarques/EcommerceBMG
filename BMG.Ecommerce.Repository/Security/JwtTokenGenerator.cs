
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities;
using Microsoft.IdentityModel.Tokens;


namespace ECommerce.Repository.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _key;
    public JwtTokenGenerator(string key) { _key = key; }
    public string Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()), new(ClaimTypes.Email, user.Email), new(ClaimTypes.Role, user.Role.ToString()) };
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(4), signingCredentials: creds);
        return tokenHandler.WriteToken(token);
    }
}