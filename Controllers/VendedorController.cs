using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendasApi.Data.Dtos.Vendedor;
using vendasApi.Models;

namespace vendasApi.Controllers;

[Controller]
[Route("[controller]")]
public class VendedorController:ControllerBase
{
    private ApiVendasContext Context;
    private IMapper Mapper;

    public VendedorController(ApiVendasContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaVendedor([FromBody] CreateVendedorDTO vendedorDTO)
    {
        Vendedor vendedor = Mapper.Map<Vendedor>(vendedorDTO);
        Context.Vendedores.Add(vendedor);
        Context.SaveChanges();
        return Ok(vendedor);
    }

    [HttpGet]
    public IEnumerable<ReadVendedorDTO> RecuperaVendedores( [FromQuery]int skip=0, int take=50)
    {
        var Vendedores = Mapper.Map<List<ReadVendedorDTO>>(Context.Vendedores.Skip(skip).Take(take).ToList());
        return Vendedores;
    }

    
}
