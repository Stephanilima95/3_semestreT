using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using ConnectPlus.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
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
                return Ok(_contatoRepository.Listar());
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
                return Ok(_contatoRepository.BuscarPorId(id));
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
        public IActionResult Cadastrar([FromForm] ContatoDTO contato)
        {
            if (String.IsNullOrEmpty(contato.Nome) || String.IsNullOrEmpty(contato.FormaContato))
            {
                return BadRequest("O nome e a forma de contato são obrigatórios");
            }
            Contato novoContato = new Contato();
            if (contato.Imagem != null && contato.Imagem.Length > 0)
            {
                var extensao = Path.GetExtension(contato.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    contato.Imagem.CopyToAsync(stream);
                }

                novoContato.Imagem = nomeArquivo;
            }
            novoContato.Nome = contato.Nome;
            novoContato.FormaContato = contato.FormaContato;
            novoContato.IdTipoContato = contato.IdTipoContato;
            try
            {
                _contatoRepository.Cadastrar(novoContato);
                return Ok(novoContato);
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
        /// <param name="contato">id do tipo evento a ser buscado</param>"
        /// <returns>Status code 204 e tipo de evento atualizado</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromForm] ContatoDTO contato)
        {
            var contatoBuscado = _contatoRepository.BuscarPorId(id);
            if (contatoBuscado == null)
            {
                return NotFound("Contato não encontrado");
            }
            if (contato.Imagem != null && contato.Imagem.Length > 0)
            {
                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);
                if (!String.IsNullOrEmpty(contatoBuscado.Imagem))
                {
                    var caminhoAntigo = Path.Combine(caminhoPasta, contatoBuscado.Imagem);
                    if (System.IO.File.Exists(caminhoAntigo))
                    {
                        System.IO.File.Delete(caminhoAntigo);
                    }
                }

                var extensao = Path.GetExtension(contato.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }
                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await contato.Imagem.CopyToAsync(stream);
                }
                contatoBuscado.Imagem = nomeArquivo;
            }
            try
            {
                _contatoRepository.Atualizar(id, contatoBuscado);
                return Ok(contatoBuscado);
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
                _contatoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
