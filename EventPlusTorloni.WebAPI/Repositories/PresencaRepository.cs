using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EventPlusTorloni.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository(EventContext eventcontext)
    {
        _eventContext = eventcontext;
    }

    public void Atualizar(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas
            .FirstOrDefault(p => p.IdPresenca == id);

        if (presencaBuscada == null)
        {
            throw new Exception("Presença não encontrada");
        }

        _eventContext.Presencas.Update(presencaBuscada);
        _eventContext.SaveChanges();
    }

    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas
            .FirstOrDefault(p => p.IdPresenca == id);

        if (presencaBuscada == null)
        {
            throw new Exception("Presença não encontrada");
        }

        _eventContext.Presencas.Remove(presencaBuscada);
        _eventContext.SaveChanges();
    }

    public void Inscrever(Presenca inscricao)
    {
        _eventContext.Presencas.Add(inscricao);
        _eventContext.SaveChanges();
    }

    public List<Presenca> Listar()
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .ToList();
    }

    public List<Presenca> ListarMinhas(Guid idUsuario)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == idUsuario)
            .ToList();
    }
}