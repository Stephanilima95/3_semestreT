using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoContatoController : ControllerBase
    {
        private ITipoContatoRepository _tipoContatoRepository;

        public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
        {
            _tipoContatoRepository = tipoContatoRepository;
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
                return Ok(_tipoContatoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint para listar os tipos de eventos cadastrados
        /// </summary>
        /// <param name="tipoContato">id do tipo evento a ser buscado</param>"
        /// <returns>Status code 201 e tipo de evento cadastrado</returns>

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_tipoContatoRepository.BuscarPorId(id));
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
        public IActionResult Cadastrar(TipoContatoDTO tipoContato)
        {
            try
            {
                var novoTipoContato = new TipoContato
                {

                    Titulo = tipoContato.Titulo!
                };

                _tipoContatoRepository.Cadastrar(novoTipoContato);
                return StatusCode(201, novoTipoContato);
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
        /// <param name="tipoContato">id do tipo evento a ser buscado</param>"
        /// <returns>Status code 204 e tipo de evento atualizado</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoContato tipoContato)
        {
            try
            {
                var tipoContatoAtualizado = new TipoContato
                {
                    Titulo = tipoContato.Titulo!
                    
                };

                _tipoContatoRepository.Atualizar(id, tipoContatoAtualizado);
                return StatusCode(204, tipoContatoAtualizado);
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
                _tipoContatoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}