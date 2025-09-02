using ECommerce.Business.Dtos;
using ECommerce.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BMG.ECommerce.Api.Controllers;

/// <summary>
/// Controlador responsável pelas operações do carrinho de compras.
/// </summary>
[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly CartService _svc;

    /// <summary>
    /// Construtor do controlador de carrinho de compras.
    /// </summary>
    /// <param name="svc">Serviço de carrinho injetado.</param>
    public CartController(CartService svc) { _svc = svc; }

    /// <summary>
    /// Adiciona um item ao carrinho do usuário autenticado.
    /// </summary>
    /// <param name="r">Objeto contendo os dados do item a ser adicionado ao carrinho.</param>
    /// <returns>Retorna o resumo do pedido após a adição do item.</returns>
    /// <response code="200">Item adicionado com sucesso ao carrinho.</response>
    /// <response code="400">Dados inválidos na requisição.</response>
    /// <response code="401">Usuário não autorizado.</response>
    [HttpPost("items"), Authorize(Roles = "Customer,Admin")]
    public async Task<IActionResult> AddItem(AddToCartRequest r)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _svc.AddItem(userId, r);
        return Ok(new { order.Id, order.Total, order.Status, Items = order.Items.Count });
    }
}
