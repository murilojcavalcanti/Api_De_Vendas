using System.ComponentModel.DataAnnotations;

namespace vendasApi.Models
{
    public class Vendedor
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
    }
}
