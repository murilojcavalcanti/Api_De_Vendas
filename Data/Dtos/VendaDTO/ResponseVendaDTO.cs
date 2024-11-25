using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vendasApi.Enums;
using vendasApi.Data.Dtos.VendedorDTO;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Data.Dtos.VendaProdutoDTO;
using vendasApi.Models;

namespace vendasApi.Data.Dtos.VendaDTO;

public class ResponseVendaDTO
{
    public int Id { get; set; }
    public StatusVendaEnum StatusVenda { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.Now;

    public ResponseVendedorDTO Vendedor { get; set; }

    public ICollection<ResponseProdutoDTO> Produtos { get; set; }
}
