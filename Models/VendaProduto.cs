using System.ComponentModel.DataAnnotations;

namespace vendasApi.Models;

public class VendaProduto
{
    [Required]
    public int VendaId { get; set; }
    public virtual Venda Venda { get; set; }

    [Required]
    public int ProdutoId { get; set; }
    public virtual Produto Produto { get; set; }
}
