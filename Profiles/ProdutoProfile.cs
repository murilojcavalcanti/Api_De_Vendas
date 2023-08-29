using AutoMapper;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<CreateProdutoDTO, Produto>();
        CreateMap<UpdateProdutoDTO, Produto>();
        CreateMap<Produto, ReadProdutoDTO>().ForMember(p=>p.VendaProdutos,opt=>opt.MapFrom(p=>p.VendaProdutos));
    }
}
