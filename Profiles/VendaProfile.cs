using AutoMapper;
using vendasApi.Data.Dtos.VendaDTO;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class VendaProfile: Profile
{
    public VendaProfile()
    {
        CreateMap<CreateVendaDTO, Venda>();
        CreateMap<Venda, ReadVendaDTO>()
            .ForMember(venda => venda.VendaProdutos,
            opt => opt.MapFrom(Venda => Venda.VendaProdutos));
            
        CreateMap<UpdateVendaDTO, Venda>();
        CreateMap<Venda, UpdateVendaDTO>();
    }
}
