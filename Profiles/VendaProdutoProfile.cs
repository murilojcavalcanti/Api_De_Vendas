using AutoMapper;
using vendasApi.Data.Dtos.VendaDTO;
using vendasApi.Data.Dtos.VendaProdutoDTO;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class VendaProdutoProfile: Profile
{
    public VendaProdutoProfile()
    {
        CreateMap<CreateVendaProdutoDTO, VendaProduto>();
        CreateMap<VendaProduto, ReadVendaProdutoDTO>();
    }
}
