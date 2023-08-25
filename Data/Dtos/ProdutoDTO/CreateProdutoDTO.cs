using System.ComponentModel.DataAnnotations;

namespace vendasApi.Data.Dtos.ProdutoDTO
{
    public class CreateProdutoDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal preço { get; set; }

        [Required]
        public string Descricao { get; set; }

    }
}
