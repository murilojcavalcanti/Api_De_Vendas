using ApiVendasApi.Data;
using Microsoft.EntityFrameworkCore;
using vendasApi.Models;

namespace vendasApi.Repositories.vendedorRepository
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {

        public VendedorRepository(ApiVendasContext context) : base(context) { }

        public IEnumerable<Vendedor> RecuperaVendedoresComVendas()
        {
            return _context.Set<Vendedor>().Include(v => v.Vendas).ToList();
        }
    }
}
