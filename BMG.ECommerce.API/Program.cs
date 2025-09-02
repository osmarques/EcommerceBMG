using BMG.Ecommerce.Repository.Repositories.Implementations;
using ECommerce.Business.Services;
using ECommerce.Domain.Abstractions;
using ECommerce.Repository.Data;
using ECommerce.Repository.Repositories;
using ECommerce.Repository.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;


builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(cfg.GetConnectionString("Default")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();


var jwtKey = cfg["Jwt:Key"] ?? throw new InvalidOperationException("JWT key missing or too short");
builder.Services.AddSingleton<IJwtTokenGenerator>(_ => new JwtTokenGenerator(jwtKey));


builder.Services.AddControllers();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
});


builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT no formato: Bearer {token}"
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
{
new OpenApiSecurityScheme
{
Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
},
Array.Empty<string>()
}
});
});


var app = builder.Build();
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await SeedData.Ensure(db);


app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();