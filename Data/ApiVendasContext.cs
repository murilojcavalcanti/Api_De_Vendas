using Microsoft.EntityFrameworkCore;
using vendasApi.Models;

namespace ApiVendasApi.Data;

public class ApiVendasContext:DbContext
{
    public ApiVendasContext(DbContextOptions<ApiVendasContext>opts):base(opts)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder Builder)
    {
        Builder.Entity<VendaProduto>().HasKey(vp => new { vp.VendaId, vp.ProdutoId });

        Builder.Entity<VendaProduto>()
            .HasOne(pv=>pv.Produto)
            .WithMany(p=>p.VendaProdutos)
            .HasForeignKey(pv=>pv.ProdutoId);

        Builder.Entity<VendaProduto>()
            .HasOne(pv => pv.Venda)
            .WithMany(p => p.VendaProdutos)
            .HasForeignKey(pv => pv.VendaId);

        base.OnModelCreating(Builder);
    }
    
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<VendaProduto> VendasProdutos { get; set; }



}
