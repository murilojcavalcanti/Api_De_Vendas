using ApiVendasApi.Data;
using vendasApi.Repositories.produtoRepository;

namespace vendasApi.Repositories.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApiVendasContext _context;

        private IProdutoRepository _produtoRepository;

        public UnitOfWork(ApiVendasContext context)
        {
            _context = context;
        }
        public IProdutoRepository ProdutoRepository { get 
            {
                return _produtoRepository = _produtoRepository?? new ProdutoRepository(_context)
            } 
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
