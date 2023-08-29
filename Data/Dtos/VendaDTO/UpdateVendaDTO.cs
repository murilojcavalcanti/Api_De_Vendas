using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vendasApi.Enums;

namespace vendasApi.Data.Dtos.VendaDTO
{
    public class UpdateVendaDTO
    {
       
        [Required]
        [DefaultValue(0)]
        public StatusVendaEnum StatusVenda { get; set; }

        
    }
}
