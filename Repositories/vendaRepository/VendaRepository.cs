using ApiVendasApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return _context.Set<Venda>().Include(v=>v.Vendedor).ToList();
        }   
    }
}
