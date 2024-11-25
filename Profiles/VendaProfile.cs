using AutoMapper;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Data.Dtos.VendaDTO;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class VendaProfile: Profile
{
    public VendaProfile()
    {
        CreateMap<CreateVendaDTO, Venda>().ReverseMap();
        CreateMap<Venda, ResponseVendaDTO>().ForMember(v=>v.Produtos,
            opt => opt.MapFrom(v => v.vendaProdutos
            .Select(vp => vp.Produto)
            .ToList()));
        CreateMap<UpdateVendaDTO, Venda>().ReverseMap();
    }
}
