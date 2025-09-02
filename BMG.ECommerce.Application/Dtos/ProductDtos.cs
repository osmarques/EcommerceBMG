namespace ECommerce.Business.Dtos;

public record ProductCreateRequest(string Name, string Description, decimal Price, int Stock);
public record ProductUpdateRequest(string Name, string Description, decimal Price, int Stock, bool Active);