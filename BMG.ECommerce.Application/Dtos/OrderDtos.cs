namespace ECommerce.Business.Dtos;

public record CheckoutRequest(string Method);
public record OrderResponse(Guid Id, decimal Total, string Status);