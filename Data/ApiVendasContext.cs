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
        modelBuilder.Entity<Vendedor>()
            .HasMany(vendedor => vendedor.Vendas)
            .WithOne(venda => venda.Vendedor)
            .HasForeignKey(venda=>venda.VendedorId)
            .IsRequired();
    }

    internal object Add()
    {
        throw new NotImplementedException();
    }

    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    
}
