using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace vendasApi.Data.Dtos.VendedorDTO
{
    public class ReadVendedorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
