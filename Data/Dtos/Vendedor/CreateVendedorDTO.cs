using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vendasApi.Data.Dtos.Vendedor;

public class CreateVendedorDTO
{
    [Required]
    public string Nome { get; set; }
    [Required]
    [MaxLength(11,ErrorMessage ="cpf Invalido"),MinLength(11,ErrorMessage ="Cpf Inválido") ]
    public string CPF { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DefaultValue("55 81 90000-0000")]
    public string Telefone { get; set; }
}
