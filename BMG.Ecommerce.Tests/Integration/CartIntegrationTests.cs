using BMG.ECommerce.Business.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using Xunit;

public class CartIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CartIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddItem_ToCart_ReturnsOk()
    {
        var product = await _client.PostAsJsonAsync("/products", new { Name = "Teste", Price = 100.0M, Stock = 10 });
        var created = await product.Content.ReadFromJsonAsync<ProductResponse>();

        var response = await _client.PostAsJsonAsync("/cart/items", new { ProductId = created.Id, Quantity = 2 });
        response.EnsureSuccessStatusCode();
    }
}