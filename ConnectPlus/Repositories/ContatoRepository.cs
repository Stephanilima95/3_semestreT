using ConnectPlus.Data;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly ConnectPlusContext _context;

        public ContatoRepository(ConnectPlusContext context)
        {
            _context = context;
        }

        public List<Contato> Listar()
        {
            return _context.Contatos.ToList();
        }

        public Contato BuscarPorId(Guid id)
        {
            return _context.Contatos.FirstOrDefault(t => t.IdContato == id)!;
        }

        public void Cadastrar(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
        }

        public void Atualizar(Guid id, Contato contato)
        {
            Contato Buscado = BuscarPorId(id);

            if (Buscado != null)
            {
                Buscado.Nome = contato.Nome;
                Buscado.FormaContato = contato.FormaContato;
                Buscado.Imagem = contato.Imagem;

                _context.Contatos.Update(Buscado);
                _context.SaveChanges();
            }
        }

        public void Deletar(Guid id)
        {
            Contato ContatoBuscado = BuscarPorId(id);

            if (ContatoBuscado != null)
            {
                _context.Contatos.Remove(ContatoBuscado);
                _context.SaveChanges();
            }
        }
    }
}
