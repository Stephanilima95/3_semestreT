using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        List<TipoUsuario> Listar();
        void Cadastrar(TipoUsuario TipoUsuario);
        void Atualizar(Guid id, TipoUsuario TipoUsuario);
        void Deletar(Guid id);
        TipoUsuario BuscarPorId(Guid id);
    }
}

