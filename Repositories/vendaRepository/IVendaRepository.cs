using Microsoft.AspNetCore.Mvc;
using vendasApi.Models;

namespace vendasApi.Repositories.vendaRepository
{
    public interface IVendaRepository:IRepository<Venda>
    {
        IEnumerable<Venda> RecuperaVendasComVendedor();
    }
}
