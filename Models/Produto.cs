using System.ComponentModel.DataAnnotations;

namespace vendasApi.Models
{
    public class Produto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal preço { get; set; }

        [Required]
        public string Descricao { get; set;}
        public ICollection<VendaProduto> vendaProdutos { get; set; } =  new List<VendaProduto>();
        public ICollection<Venda> Vendas { get; set; }

    }
}
