using System.ComponentModel.DataAnnotations;

namespace vendasApi.Data.Dtos.ProdutoDTO;

public class ReadProdutoDTO
{
    public int id { get; set; }
    public string Nome { get; set; }
    public decimal preço { get; set; }
    public string Descricao { get; set; }

}
