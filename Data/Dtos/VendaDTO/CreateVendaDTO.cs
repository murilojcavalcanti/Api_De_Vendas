using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vendasApi.Data.Dtos.VendedorDTO;
using vendasApi.Enums;
using vendasApi.Models;

namespace vendasApi.Data.Dtos.VendaDTO;

public class CreateVendaDTO
{ 
    [Required]
    public int VendedorId { get; set; }

    public DateTime DataPedido { get; set; } = DateTime.Now;
}
