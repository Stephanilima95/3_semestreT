using ConnectPlus.Models;

namespace ConnectPlus.Interfaces
{
    public interface ITipoContatoRepository
    {
        List<TipoContato> Listar();

        TipoContato BuscarPorId(Guid id);

        void Cadastrar(TipoContato tipoContato);

        void Atualizar(Guid id, TipoContato tipoContato);

        void Deletar(Guid id);
    }
}