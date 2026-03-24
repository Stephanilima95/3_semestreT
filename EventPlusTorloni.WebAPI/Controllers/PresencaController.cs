using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private readonly IPresencaRepository _presencaRepository;
    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que retorna a presença de um usuário em um evento específico, identificada pelo seu ID único.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status code 200 e presenca buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            var presenca = _presencaRepository.BuscarPorId(id);
            if (presenca == null)
            {
                return NotFound();
            }
            return Ok(presenca);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que retorna uma lista de presenças associadas a um usuário específico, identificado pelo seu ID único.
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns>Uma lista de presenca filtard pelo usuario</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));            
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdEvento = presenca.IdEvento,
                IdUsuario = presenca.IdUsuario
            };
            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201, novaPresenca); // Created
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // ✅ Atualizar presença
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id)
    {
        try
        {
            _presencaRepository.Atualizar(id);
            return NoContent(); // 204
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // ✅ Deletar presença
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent(); // 204
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
