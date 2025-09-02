using ECommerce.Business.Dtos;
using ECommerce.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BMG.ECommerce.Api.Controllers;

/// <summary>
/// Controlador responsável pelas operações relacionadas a pedidos.
/// </summary>
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _svc;

    /// <summary>
    /// Construtor do controlador de pedidos.
    /// </summary>
    /// <param name="svc">Serviço de pedidos injetado.</param>
    public OrdersController(OrderService svc) { _svc = svc; }

    /// <summary>
    /// Realiza o checkout do carrinho, finalizando o pedido do usuário autenticado.
    /// </summary>
    /// <param name="r">Dados necessários para finalizar o pedido.</param>
    /// <returns>Retorna os dados do pedido finalizado.</returns>
    /// <response code="200">Pedido finalizado com sucesso.</response>
    /// <response code="400">Dados inválidos na requisição.</response>
    /// <response code="401">Usuário não autorizado.</response>
    [HttpPost("checkout"), Authorize(Roles = "Customer,Admin")]
    public async Task<IActionResult> Checkout(CheckoutRequest r)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _svc.Checkout(userId, r));
    }

    /// <summary>
    /// Retorna o histórico de pedidos do usuário autenticado.
    /// </summary>
    /// <returns>Lista de pedidos realizados pelo usuário.</returns>
    /// <response code="200">Histórico de pedidos retornado com sucesso.</response>
    /// <response code="401">Usuário não autorizado.</response>
    [HttpGet("history"), Authorize(Roles = "Customer,Admin")]
    public async Task<IActionResult> History()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var items = await _svc.History(userId);
        return Ok(items);
    }
}
