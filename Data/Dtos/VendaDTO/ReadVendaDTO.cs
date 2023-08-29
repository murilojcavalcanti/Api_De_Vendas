using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vendasApi.Enums;
using vendasApi.Data.Dtos.VendedorDTO;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Data.Dtos.VendaProdutoDTO;

namespace vendasApi.Data.Dtos.VendaDTO;

public class ReadVendaDTO
{
    public int Id { get; set; }
    public StatusVendaEnum StatusVenda { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.Now;

    public ReadVendedorDTO Vendedor { get; set; }

    public ICollection<ReadVendaProdutoDTO> VendaProdutos { get; set; }

}
