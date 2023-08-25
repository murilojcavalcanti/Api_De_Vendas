using AutoMapper;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<UpdateProdutoDTO, Produto>();
        CreateMap<Produto, ReadProdutoDTO>();
    }
}
