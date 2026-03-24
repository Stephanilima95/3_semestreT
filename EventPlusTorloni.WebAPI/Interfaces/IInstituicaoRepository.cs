using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces
{
    public interface IInstituicaoRepository
    {
        List<Instituicao> Listar();
        void Cadastrar(Instituicao Instituicao);
        void Atualizar(Guid id, Instituicao Instituicao);
        void Deletar(Guid id);
        Instituicao BuscarPorId(Guid id);
    }
}

