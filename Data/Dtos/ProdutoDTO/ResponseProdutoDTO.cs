using System.ComponentModel.DataAnnotations;
using vendasApi.Data.Dtos.VendaProdutoDTO;

namespace vendasApi.Data.Dtos.ProdutoDTO
{
    public class ResponseProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal preço { get; set; }
        public string Descricao { get; set; }
    }
}
