using System.ComponentModel.DataAnnotations;

namespace vendasApi.Data.Dtos.ProdutoDTO
{
    public class UpdateProdutoDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal preço { get; set; }

        [Required]
        public string Descricao { get; set; }

    }
}
