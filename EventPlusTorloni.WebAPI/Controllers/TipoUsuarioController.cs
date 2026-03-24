using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        /// <summary>
        /// Lista todos os tipos de usuários cadastrados
        /// </summary>
        /// <returns>Status code 200 e lista de tipos de usuários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_tipoUsuarioRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Busca um tipo de usuário pelo ID
        /// </summary>
        /// <param name="id">Id do tipo usuário</param>
        /// <returns>Status code 200 e o tipo usuário encontrado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                var tipoUsuario = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoUsuario == null)
                {
                    return NotFound();
                }

                return Ok(tipoUsuario);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="tipoUsuario">Dados do tipo usuário</param>
        /// <returns>Status code 201</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
        {
            try
            {
                TipoUsuario novoTipoUsuario = new TipoUsuario
                {
                    Titulo = tipoUsuario.Titulo!
                };

                _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);

                return StatusCode(201, novoTipoUsuario);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Atualiza um tipo de usuário existente
        /// </summary>
        /// <param name="id">Id do tipo usuário</param>
        /// <param name="tipoUsuario">Dados atualizados</param>
        /// <returns>Status code 204</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
        {
            try
            {
                TipoUsuario tipoUsuarioAtualizado = new TipoUsuario
                {
                    Titulo = tipoUsuario.Titulo!
                };

                _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Deleta um tipo de usuário
        /// </summary>
        /// <param name="id">Id do tipo usuário</param>
        /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _tipoUsuarioRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}