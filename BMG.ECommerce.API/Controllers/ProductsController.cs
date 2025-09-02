using ECommerce.Business.Dtos;
using ECommerce.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BMG.ECommerce.Api.Controllers;

/// <summary>
/// Controlador responsável pela gestão de produtos na plataforma.
/// </summary>
[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _svc;

    /// <summary>
    /// Construtor do controlador de produtos.
    /// </summary>
    /// <param name="svc">Serviço de produtos injetado.</param>
    public ProductsController(ProductService svc) { _svc = svc; }

    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    /// <param name="r">Dados do produto a ser criado.</param>
    /// <returns>Retorna o produto criado.</returns>
    /// <response code="200">Produto criado com sucesso.</response>
    /// <response code="400">Dados inválidos na requisição.</response>
    /// <response code="401">Usuário não autorizado.</response>
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(ProductCreateRequest r) => Ok(await _svc.Create(r));

    /// <summary>
    /// Lista todos os produtos disponíveis.
    /// </summary>
    /// <returns>Lista de produtos.</returns>
    /// <response code="200">Produtos retornados com sucesso.</response>
    [HttpGet]
    public async Task<IActionResult> List() => Ok(await _svc.List());

    /// <summary>
    /// Retorna os detalhes de um produto específico.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <returns>Produto correspondente ao ID.</returns>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="404">Produto não encontrado.</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var p = await _svc.Get(id);
        return p is null ? NotFound() : Ok(p);
    }

    /// <summary>
    /// Atualiza os dados de um produto existente.
    /// </summary>
    /// <param name="id">ID do produto a ser atualizado.</param>
    /// <param name="r">Novos dados do produto.</param>
    /// <returns>Retorna o produto atualizado.</returns>
    /// <response code="200">Produto atualizado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="401">Usuário não autorizado.</response>
    [HttpPut("{id:guid}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, ProductUpdateRequest r) => Ok(await _svc.Update(id, r));

    /// <summary>
    /// Exclui um produto pelo ID.
    /// </summary>
    /// <param name="id">ID do produto a ser excluído.</param>
    /// <returns>Sem conteúdo.</returns>
    /// <response code="204">Produto excluído com sucesso.</response>
    /// <response code="401">Usuário não autorizado.</response>
    [HttpDelete("{id:guid}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _svc.Delete(id);
        return NoContent();
    }
}
