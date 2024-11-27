using ApiVendasApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using vendasApi.Models;

namespace vendasApi.Repositories.vendaRepository
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        public VendaRepository(ApiVendasContext context) : base(context)
        {
        }
        
        public IEnumerable<Venda> RecuperaVendasComVendedor()
        {
            return _context.Set<Venda>()
                .AsNoTracking()
                .Include(v => v.Vendedor)
                .Include(v => v.vendaProdutos)
                .ThenInclude(vp => vp.Produto)
                .ToList();
        }
        public Venda RecuperaVendaComVendedor(int id)
        {
            return _context.Set<Venda>()
                .Include(v => v.Vendedor)
                .Include(v => v.vendaProdutos)
                .ThenInclude(vp => vp.Produto)
                .FirstOrDefault(v=>v.Id==id);
        }

        public Venda RecuperaVendaPorVendedor(int id)
        {
            return _context.Set<Venda>()
                .Include(v => v.Vendedor)
                .Include(v => v.vendaProdutos)
                .ThenInclude(vp => vp.Produto)
                .FirstOrDefault(v => v.Vendedor.Id == id);
        }
        public Venda RecuperaVendaPorData(DateTime dateTime)
        {
            return _context.Set<Venda>()
                .Include(v => v.Vendedor)
                .Include(v => v.vendaProdutos)
                .ThenInclude(vp => vp.Produto)
                .FirstOrDefault(v => v.DataPedido.Date== dateTime);
        }

        public VendaProduto AdicionaVendaProduto(Venda venda,Produto produto)
        {
            VendaProduto vendaProduto = new VendaProduto()
            {
                VendaId = venda.Id,
                ProdutoId = produto.Id
            };
             _context.Set<VendaProduto>().Add(vendaProduto);
            return vendaProduto;
        }
    }
}
