using Microsoft.EntityFrameworkCore;
using vendasApi.Models;

namespace ApiVendasApi.Data;

public class ApiVendasContext:DbContext
{
    public ApiVendasContext(DbContextOptions<ApiVendasContext>opts):base(opts)
    {
        
    }
    /*protected override void OnModelCreating(ModelBuilder Builder)
    {
        Builder.Entity<Venda>().HasKey(venda => new { venda.VendedorId, venda.ProdutoId });

    }
    */
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    //public DbSet<Venda> Vendas { get; set; }
    


}
