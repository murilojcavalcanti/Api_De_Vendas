using vendasApi.Repositories.produtoRepository;
using vendasApi.Repositories.vendedorRepository;

namespace vendasApi.Repositories.unitOfWork
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        IVendedorRepository VendedorRepository { get; }

        void Commit();
    }
}
