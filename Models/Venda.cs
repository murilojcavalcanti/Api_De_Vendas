using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public virtual Vendedor Vendedor { get; set; }
        public  virtual ICollection<VendaProduto> VendaProdutos { get; set; }
    }
}
