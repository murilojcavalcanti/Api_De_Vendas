using AutoMapper;
using vendasApi.Data.Dtos.VendedorDTO;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class VendedorProfile: Profile
{
    public VendedorProfile()
    {
        CreateMap<CreateVendedorDTO, Vendedor>();
        CreateMap<Vendedor, ResponseVendedorDTO>();
        CreateMap<UpdateVendedor, Vendedor>();
    }
}
