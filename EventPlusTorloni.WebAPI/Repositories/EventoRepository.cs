using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventPlusTorloni.WebAPI.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly EventContext _context;

        public EventoRepository(EventContext context)
        {
            _context = context;
        }

        public List<Evento> Listar()
        {
            // Usando .Eventos (plural) conforme o padrão EF, ajuste se no Context estiver .Evento
            return _context.Eventos
                .Include(e => e.IdTipoEventoNavigation)
                .Include(e => e.IdInstituicaoNavigation)
                .ToList();
        }

        public void Cadastar(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        public void Deletar(Guid IdEvento)
        {
            // Busca o evento correto pelo ID
            var eventoBuscado = _context.Eventos.Find(IdEvento);
            if (eventoBuscado != null)
            {
                _context.Eventos.Remove(eventoBuscado);
                _context.SaveChanges();
            }
        }

        public void Atualizar(Guid id, Evento evento)
        {
            var eventoBuscado = _context.Eventos.Find(id);

            if (eventoBuscado != null)
            {
                // Atualiza apenas os campos da tabela Evento
                eventoBuscado.Nome = !string.IsNullOrWhiteSpace(evento.Nome) ? evento.Nome : eventoBuscado.Nome;
                eventoBuscado.Descricao = !string.IsNullOrWhiteSpace(evento.Descricao) ? evento.Descricao : eventoBuscado.Descricao;
                eventoBuscado.DataEvento = evento.DataEvento;

                // FKs geralmente não são nulas se vierem de um formulário
                if (evento.IdTipoEvento != Guid.Empty) eventoBuscado.IdTipoEvento = evento.IdTipoEvento;
                if (evento.IdInstituicao != Guid.Empty) eventoBuscado.IdInstituicao = evento.IdInstituicao;

                _context.Eventos.Update(eventoBuscado);
                _context.SaveChanges();
            }
        }

        public Evento BuscarPorId(Guid id)
        {
            // Retorna o evento com as navegações incluídas
            return _context.Eventos
                .Include(e => e.IdTipoEventoNavigation)
                .Include(e => e.IdInstituicaoNavigation)
                .FirstOrDefault(e => e.IdEvento == id)!;
        }

        public List<Evento> ListaPorId(Guid IdUsuario)
        {
            return _context.Eventos
                .Include(e => e.IdInstituicaoNavigation)
                .Include(e => e.IdTipoEventoNavigation)
                // Certifique-se que a tabela intermediária 'Presencas' existe no seu Domain
                .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
                .ToList();
        }

        public List<Evento> ProximosEventos()
        {
            // Nota: Sua tabela SQL não tem coluna 'DataEvento', 
            // verifique se ela existe no seu Domain ou se deve ser adicionada ao SQL.
            return _context.Eventos.Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now).OrderBy(e => e.DataEvento).ToList();
        }
    }
}
