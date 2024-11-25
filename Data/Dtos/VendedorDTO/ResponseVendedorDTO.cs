using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vendasApi.Models;

namespace vendasApi.Data.Dtos.VendedorDTO
{
    public class ResponseVendedorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
