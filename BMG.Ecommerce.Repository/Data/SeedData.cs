using System.Data;
using System.Security.Cryptography;
using System.Text;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository.Data;

public static class SeedData
{
    public static async Task Ensure(AppDbContext db)
    {
        await db.Database.EnsureCreatedAsync();
        if (!await db.Users.AnyAsync())
        {
            var admin = new User { Id = Guid.NewGuid(), Email = "admin@shop.local", PasswordHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("admin123"))), Role = Role.Admin };
            var user = new User { Id = Guid.NewGuid(), Email = "user@shop.local", PasswordHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("user123"))), Role = Role.Customer };
            await db.Users.AddRangeAsync(admin, user);
        }
        if (!await db.Products.AnyAsync())
        {
            await db.Products.AddRangeAsync(
                new Product { Id = Guid.NewGuid(), Name = "Notebook", Description = "14", Price = 3500, Stock = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Mouse", Description = "Wireless", Price = 80, Stock = 100 }
            );
        }
        await db.SaveChangesAsync();
    }
}