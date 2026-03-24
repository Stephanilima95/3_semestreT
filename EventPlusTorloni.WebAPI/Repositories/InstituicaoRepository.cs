using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusTorloni.WebAPI.Repositories
{
    public class InstituicaoRepository : IInstituicaoRepository
    {
        private readonly EventContext _context;

        public InstituicaoRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Buscar um tipo de evento por id
        /// </summary>
        /// <param name="id">id do tipo evento a ser buscado</param>
        /// <returns>Objeto do tipoEvento com as informações do tipo de evento buscado</returns>
        /// 
        public void Atualizar(Guid id, Models.Instituicao instituicao)
        {
            var instituicaoBuscado = _context.Instituicaos.Find(id);

            if (instituicaoBuscado != null)
            {
                instituicaoBuscado.NomeFantasia = String.IsNullOrWhiteSpace(instituicao.NomeFantasia) ? instituicaoBuscado.NomeFantasia :
                    instituicao.NomeFantasia;
                instituicaoBuscado.Cnpj = String.IsNullOrWhiteSpace(instituicao.Cnpj) ? instituicaoBuscado.Cnpj :
                    instituicao.Cnpj;
                instituicaoBuscado.Endereco = String.IsNullOrWhiteSpace(instituicao.Endereco) ? instituicaoBuscado.Endereco :
                    instituicao.Endereco;
                _context.SaveChanges();
            }
        }


        public void Cadastrar(Instituicao instituicao)
        {
            _context.Instituicaos.Add(instituicao);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);
            if (tipoUsuarioBuscado != null)
            {
                _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
                _context.SaveChanges();
            }
        }

        Instituicao IInstituicaoRepository.BuscarPorId(Guid id)
        {
            return _context.Instituicaos.Find(id)!;
        }

        List<Instituicao> IInstituicaoRepository.Listar()
        {
            return _context.Instituicaos.ToList();
        }
    }
}

