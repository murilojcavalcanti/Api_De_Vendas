using AutoMapper;
using vendasApi.Data.Dtos.Vendedor;
using vendasApi.Models;

namespace vendasApi.Profiles;

public class VendedorProfile: Profile
{
    public VendedorProfile()
    {
        CreateMap<CreateVendedorDTO, Vendedor>();
    }
}
