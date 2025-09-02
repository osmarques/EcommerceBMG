namespace ECommerce.Business.Dtos;

public record AddToCartRequest(Guid ProductId, int Quantity);