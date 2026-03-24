using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces;

public interface ITipoEventoRepository
{
    List<TipoEvento> Listar();
    void Atualizar(Guid id, TipoEvento tipoEvento);
    void Deletar(Guid id);
    TipoEvento BuscarPorId(Guid id);
    void Cadastrar(TipoEvento tipoEvento);
}
