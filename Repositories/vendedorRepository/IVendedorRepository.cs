using Microsoft.EntityFrameworkCore;
using vendasApi.Models;

namespace vendasApi.Repositories.vendedorRepository
{
    public interface IVendedorRepository:IRepository<Vendedor>
    {
        public IEnumerable<Vendedor> RecuperaVendedoresComVendas();
        

    }
}
