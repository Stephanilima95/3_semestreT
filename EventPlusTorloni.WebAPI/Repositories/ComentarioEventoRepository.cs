using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioRepository
{
    private readonly EventContext _context;
    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        var comentarioBuscado = _context.ComentarioEventos.FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento);
        if (comentarioBuscado != null)
        {
            return comentarioBuscado;
        }
        else
        {
            return null!;
        }
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var comentarioBuscado = _context.ComentarioEventos.Find(id);
        if (comentarioBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioBuscado);
            _context.SaveChanges();
        }
    }

    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.ComentarioEventos.Where(c => c.IdEvento == IdEvento).ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        throw new NotImplementedException();
    }
}
