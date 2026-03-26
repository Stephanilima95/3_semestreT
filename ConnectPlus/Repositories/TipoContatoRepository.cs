using ConnectPlus.Data;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories
{
    public class TipoContatoRepository : ITipoContatoRepository
    {
        private readonly ConnectPlusContext _context;

        public TipoContatoRepository(ConnectPlusContext context)
        {
            _context = context;
        }

        public List<TipoContato> Listar()
        {
            return _context.TipoContatos.ToList();
        }

        public TipoContato BuscarPorId(Guid id)
        {
            return _context.TipoContatos.FirstOrDefault(t => t.IdTipoContato == id)!;
        }

        public void Cadastrar(TipoContato tipoContato)
        {
            _context.TipoContatos.Add(tipoContato);
            _context.SaveChanges();
        }

        public void Atualizar(Guid id, TipoContato tipoContato)
        {
            TipoContato tipoBuscado = BuscarPorId(id);

            if (tipoBuscado != null)
            {
                tipoBuscado.Titulo = tipoContato.Titulo;

                _context.TipoContatos.Update(tipoBuscado);
                _context.SaveChanges();
            }
        }

        public void Deletar(Guid id)
        {
            TipoContato tipoBuscado = BuscarPorId(id);

            if (tipoBuscado != null)
            {
                _context.TipoContatos.Remove(tipoBuscado);
                _context.SaveChanges();
            }
        }
    }
}