using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendasApi.Data.Dtos.VendedorDTO;
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
    /// <summary>
    /// Adiciona um vendedor ao banco de dados
    /// </summary>
    /// <param name="vendedorDto">Objeto com os campos necessários para criação de um vendedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

    /// <summary>
    /// Retorna Vendedores adicionados ao banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisisção seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadVendedorDTO> RecuperaVendedores( )
    {
        var Vendedores = Mapper.Map<List<ReadVendedorDTO>>(Context.Vendedores.ToList());
        return Vendedores;
    }

    /// <summary>
    /// Retorna um vendedor com o indice escolhido que está no banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar o vendedor com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisisção seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaVendedorPorId( int id)
    {
        var vendedor = Context.Vendedores.FirstOrDefault(vendedor=>vendedor.id == id);
        if(vendedor is null) return NotFound();
        var vendedorDto = Mapper.Map<ReadVendedorDTO>(vendedor);
        return Ok(vendedorDto);
    }

    /// <summary>
    /// Atualiza um vendedor do banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar o vendedor com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisisção seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AtulizaVendedor(int id,[FromBody] UpdateVendedorDTO vendedorDTO)
    {
        var vendedor = Context.Vendedores.FirstOrDefault(v=>v.id==id);
        if(vendedor is null) return NotFound();
        Mapper.Map(vendedorDTO, vendedor);
        Context.SaveChanges();
        return Ok(vendedorDTO);
    }

    /// <summary>
    /// Remove o vendedor do banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar o vendedor com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a requisisção seja feita com sucesso</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
