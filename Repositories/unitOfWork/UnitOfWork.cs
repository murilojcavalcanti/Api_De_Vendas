using ApiVendasApi.Data;
using vendasApi.Repositories.produtoRepository;
using vendasApi.Repositories.vendaRepository;
using vendasApi.Repositories.vendedorRepository;

namespace vendasApi.Repositories.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApiVendasContext _context;

        private IProdutoRepository _produtoRepository;
        private IVendedorRepository _vendedorRepository;
        private IVendaRepository _vendaRepository;

        public UnitOfWork(ApiVendasContext context)
        {
            _context = context;
        }
        public IProdutoRepository ProdutoRepository { get 
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            } 
        }

        public IVendedorRepository VendedorRepository { get 
            {
                return _vendedorRepository = _vendedorRepository ?? new VendedorRepository(_context);
            } 
        }

        public IVendaRepository VendaRepository { get 
            {
                return _vendaRepository = _vendaRepository?? new VendaRepository(_context);
            } }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
