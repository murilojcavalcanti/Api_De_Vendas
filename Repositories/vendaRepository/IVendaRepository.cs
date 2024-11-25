using Microsoft.AspNetCore.Mvc;
using vendasApi.Models;

namespace vendasApi.Repositories.vendaRepository
{
    public interface IVendaRepository:IRepository<Venda>
    {
        public IEnumerable<Venda> RecuperaVendasComVendedor();
        public VendaProduto AdicionaVendaProduto(Venda venda, Produto produto);
    }
}
