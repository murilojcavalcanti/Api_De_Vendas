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
        return CreatedAtAction(nameof(RecuperaVendedorPorId),
            new
            {
                id = vendedor.id
            }, vendedorDTO);
    }

    [HttpGet]
    public IEnumerable<ReadVendedorDTO> RecuperaVendedores( )
    {
        var Vendedores = Mapper.Map<List<ReadVendedorDTO>>(Context.Vendedores.ToList());
        return Vendedores;
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaVendedorPorId( int id)
    {
        var vendedor = Context.Vendedores.FirstOrDefault(vendedor=>vendedor.id == id);
        if(vendedor is null) return NotFound();
        var vendedorDto = Mapper.Map<ReadVendedorDTO>(vendedor);
        return Ok(vendedorDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtulizaVendedor(int id,[FromBody] UpdateVendedorDTO vendedorDTO)
    {
        var vendedor = Context.Vendedores.FirstOrDefault(v=>v.id==id);
        if(vendedor is null) return NotFound();
        Mapper.Map(vendedorDTO, vendedor);
        Context.SaveChanges();
        return Ok(vendedorDTO);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarVendedor(int id)
    {
        var vendedor = RecuperaVendedorPorId(id);
        if( vendedor is null) return NotFound();
        Context.Remove(vendedor);
        Context.SaveChanges();
        return NoContent();

    }
}
