using ECommerce.Business.Dtos;
using ECommerce.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BMG.ECommerce.Api.Controllers;

/// <summary>
/// Controlador responsável por autenticação de usuários.
/// </summary>
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _svc;

    /// <summary>
    /// Construtor do controlador de autenticação.
    /// </summary>
    /// <param name="svc">Serviço de autenticação injetado.</param>
    public AuthController(AuthService svc) { _svc = svc; }

    /// <summary>
    /// Realiza o login do usuário.
    /// </summary>
    /// <param name="req">Objeto contendo os dados de login (usuário e senha).</param>
    /// <returns>Retorna um token JWT e informações do usuário autenticado, se as credenciais forem válidas.</returns>
    /// <response code="200">Login realizado com sucesso.</response>
    /// <response code="400">Requisição inválida.</response>
    /// <response code="401">Credenciais inválidas.</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req) => Ok(await _svc.Login(req));
}
