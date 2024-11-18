using ApiVendasApi.Data;
using vendasApi.Models;

namespace vendasApi.Repositories.produtoRepository

{
    public class ProdutoRepository:Repository<Produto>,IProdutoRepository
    {
        public ProdutoRepository(ApiVendasContext context):base(context) { }
        
    }
}
