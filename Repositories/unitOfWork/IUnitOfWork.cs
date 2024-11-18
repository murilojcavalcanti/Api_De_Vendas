using vendasApi.Repositories.produtoRepository;

namespace vendasApi.Repositories.unitOfWork
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }

        void Commit();
    }
}
