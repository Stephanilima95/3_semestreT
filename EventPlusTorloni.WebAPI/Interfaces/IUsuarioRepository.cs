using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario Usuario);
    Usuario BuscarPorId(Guid id);
    Usuario BuscarPorEmailESenha(string email, string senha, Guid IdTipoUsuario);
}

