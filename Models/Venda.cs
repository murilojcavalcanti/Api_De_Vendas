using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Enums;

namespace vendasApi.Models
{
    public class Venda
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DefaultValue(StatusVendaEnum.AguardandoPagamento)]
        public StatusVendaEnum StatusVenda { get; set; }

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Required]
        public int VendedorId { get; set; }
        
        [JsonIgnore]
        public Vendedor Vendedor { get; set; }

        public ICollection<VendaProduto> vendaProdutos { get; set; } = new List<VendaProduto>();
        public ICollection<Produto> Produtos { get; set; }
    }
}
