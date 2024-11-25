using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vendasApi.Models;

namespace vendasApi.Repositories.vendaRepository
{
    public interface IVendaRepository:IRepository<Venda>
    {
        public IEnumerable<Venda> RecuperaVendasComVendedor();
        public VendaProduto AdicionaVendaProduto(Venda venda, Produto produto);
        public Venda RecuperaVendaPorVendedor(int id);
        public Venda RecuperaVendaPorData(DateTime dateTime);
    }
}
