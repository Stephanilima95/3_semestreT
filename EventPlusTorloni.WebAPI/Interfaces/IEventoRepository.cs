using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces
{
    public interface IEventoRepository
    {
        void Cadastar(Evento evento);
        List<Evento> Listar();
        void Deletar(Guid IdEvento);
        void Atualizar(Guid id, Evento evento);
        List<Evento> ListaPorId(Guid id);
        List<Evento> ProximosEventos();
    }
}
