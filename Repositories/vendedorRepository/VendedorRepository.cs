using ApiVendasApi.Data;
using vendasApi.Models;

namespace vendasApi.Repositories.vendedorRepository
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {

        public VendedorRepository(ApiVendasContext context):base(context) { }
        
    }
}
