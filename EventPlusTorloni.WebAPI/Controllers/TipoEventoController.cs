using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EventPlusTorloni.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventoRepository;

    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }


    /// <summary>
    /// Endpoint para listar os tipos de eventos cadastrados
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>"
    /// <returns>Status code 200 e tipo de evento buscado</returns>

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint para listar os tipos de eventos cadastrados
    /// </summary>
    /// <param name="tipoEvento">id do tipo evento a ser buscado</param>"
    /// <returns>Status code 201 e tipo de evento cadastrado</returns>

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok (_tipoEventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint para listar os tipos de eventos cadastrados
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>"
    /// <returns>Status code 200 e tipo de evento buscado</returns>

    [HttpPost]
    public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
              
                Titulo = tipoEvento.Titulo!
            };

            _tipoEventoRepository.Cadastrar(novoTipoEvento);
            return StatusCode(201, novoTipoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint para atualização dos tipos de eventos cadastrados
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>"
    /// <param name="tipoEvento">id do tipo evento a ser buscado</param>"
    /// <returns>Status code 204 e tipo de evento atualizado</returns>
    [HttpPut("{id}")]
     public IActionResult Atualizar(Guid id, TipoEvento tipoEvento)
        {
            try
            {
            var tipoEventoAtualizado = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };

                _tipoEventoRepository.Atualizar(id, tipoEventoAtualizado);
                return StatusCode(204, tipoEventoAtualizado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
    }

    /// <summary>
    /// Endpoint para atualização dos tipos de eventos cadastrados
    /// </summary>
    /// <param name="id">id do tipo evento a ser excluido</param>"
    /// <returns>Status code 204 e tipo de evento deletado</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
