using System.ComponentModel.DataAnnotations;

namespace vendasApi.Models;

public class VendaProduto
{
    [Required]
    public int VendaId { get; set; }
    public Venda Venda { get; set; }

    [Required]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }

}
