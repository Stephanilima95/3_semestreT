using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private Usuario usuario;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a busca de um usuário por email e senha, retornando o usuário encontrado ou um erro caso as credenciais sejam inválidas.
    /// </summary>
    /// <param name="id"></param>
    /// <returnsStatusCode 200 e o usuario buscado></returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);

        }
    }

    /// <summary>
    /// Endpoint da API que faz a busca de um usuário por email e senha, retornando o usuário encontrado ou um erro caso as credenciais sejam inválidas.
    /// </summary>
    /// <param name="usuario"></param>
    /// <returnsStatusCode 200 e o usuario buscado></returns>
    [HttpPost]
    public IActionResult Cadastrar(UsuarioDTO usuario)
    {
        try
        {
            var usuarioDTO = new Usuario
            {
                Nome = usuario.Nome!,
                Email = usuario.Email!,
                Senha = usuario.Senha!,
                IdTipoUsuario = usuario.IdTipoUsuario
            };
            _usuarioRepository.Cadastrar(usuarioDTO);
            return StatusCode(201, usuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


}
