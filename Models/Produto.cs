using System.ComponentModel.DataAnnotations;

namespace vendasApi.Models
{
    public class Produto
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal preço { get; set; }

        [Required]
        public string Descricao { get; set;}

        [Required]
        public int Quantidade { get; set; }
    }
}
