using vendasApi.Repositories.produtoRepository;
using vendasApi.Repositories.vendaRepository;
using vendasApi.Repositories.vendedorRepository;

namespace vendasApi.Repositories.unitOfWork
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        IVendedorRepository VendedorRepository { get; }

        IVendaRepository VendaRepository { get; }
        void Commit();
    }
}
