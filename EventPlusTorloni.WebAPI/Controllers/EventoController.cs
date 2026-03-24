using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;
        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        [HttpGet("Usuario/{IdUsuario}")]
        public IActionResult ListarEventosPorUsuario(Guid IdUsuario)
        {
            try
            {
                return Ok(_eventoRepository.ListaPorId(IdUsuario));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que retorna uma lista de eventos futuros, ou seja, eventos cuja data de realização é posterior à data atual. O método busca os eventos no repositório e retorna apenas aqueles que ainda não ocorreram, permitindo aos usuários visualizar os próximos eventos disponíveis.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListarProximos")]
        public IActionResult BuscarProximosEventos()
        {
            try
            {
                return Ok(_eventoRepository.ProximosEventos());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamada para o método de cadastrar um evento
        /// </summary>
        /// <param name="evento">Dados do evento cadastrado</param>
        /// <returns>Status code 201</returns>
        [HttpPost]
        public IActionResult Cadastrar(EventoDTO evento)
        {
            try
            {
                var novoEvento = new Evento
                {
                    Nome = evento.Nome!,
                    Descricao = evento.Descricao!,
                    DataEvento = evento.DataEvento,
                    IdTipoEvento = evento.IdTipoEvento,
                    IdInstituicao = evento.IdInstituicao
                };
               _eventoRepository.Cadastar(novoEvento);
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que faz chamada para o método de listar todos os eventos
        /// </summary>
        /// <returns>Status code 200 e lista com todos os eventos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_eventoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que faz chamada para o método de atualizar um evento
        /// </summary>
        /// <param name="id">Id do evento a ser atualizado</param>
        /// <param name="evento">Dados do evento atualizado</param>
        /// <returns>Status code 204 e evento com os dados atualizados</returns>
        [HttpPut]
        public IActionResult Atualizar(Guid id, EventoDTO evento)
        {
            try
            {
                var eventoAtualizado = new Evento
                {
                    IdEvento = Guid.NewGuid(),
                    Nome = evento.Nome!,
                    Descricao = evento.Descricao!,
                    DataEvento = evento.DataEvento,
                    IdTipoEvento = evento.IdTipoEvento,
                    IdInstituicao = evento.IdInstituicao
                };
                _eventoRepository.Atualizar(id, eventoAtualizado);
                return StatusCode(204, eventoAtualizado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que faz chamada para o método de deletar um evento
        /// </summary>
        /// <param name="id">Id do evento a ser deletado</param>
        /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _eventoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }



    }
}
