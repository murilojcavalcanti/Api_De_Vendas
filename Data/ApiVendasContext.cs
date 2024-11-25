using Microsoft.EntityFrameworkCore;
using vendasApi.Models;

namespace ApiVendasApi.Data;

public class ApiVendasContext:DbContext
{
    public ApiVendasContext(DbContextOptions<ApiVendasContext>opts):base(opts)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VendaProduto>().HasKey(vp =>
        new {
            vp.VendaId,
            vp.ProdutoId
        });
        modelBuilder.Entity<VendaProduto>()
            .HasOne(vp => vp.Venda)
            .WithMany(venda => venda.vendaProdutos)
            .HasForeignKey(vp => vp.VendaId);

        modelBuilder.Entity<VendaProduto>()
            .HasOne(vp => vp.Produto)
            .WithMany(produto => produto.vendaProdutos)
            .HasForeignKey(vp => vp.ProdutoId);

        modelBuilder.Entity<Vendedor>()
            .HasMany(vendedor => vendedor.Vendas)
            .WithOne(venda => venda.Vendedor)
            .HasForeignKey(venda=>venda.VendedorId)
            .IsRequired();

    }

    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<VendaProduto> VendaProdutos { get; set; }
}
