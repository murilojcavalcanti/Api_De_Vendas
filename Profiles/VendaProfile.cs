using AutoMapper;
using vendasApi.Data.Dtos.VendaDTO;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class VendaProfile: Profile
{
    public VendaProfile()
    {
        CreateMap<CreateVendaDTO, Venda>().ReverseMap();
        CreateMap<Venda, ResponseVendaDTO>().ReverseMap();
        CreateMap<UpdateVendaDTO, Venda>().ReverseMap();
    }
}
