using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Models;

namespace vendasApi.Controllers;
[ApiController]

[Route("[controller]")]
public class ProdutoController:ControllerBase
{
    private ApiVendasContext Context;
    private IMapper Mapper;

    public ProdutoController(ApiVendasContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    
    /// <summary>
    /// Adiciona um Produto ao banco de dados
    /// </summary>
    /// <param name="ProdutoDto">Objeto com os campos necessários para criação de um vendedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaProduto(UpdateProdutoDTO produtoDTO)
    {
        var produto = Mapper.Map<Produto>(produtoDTO);
        Context.Add(produto);
        Context.SaveChanges();
        return Ok(produto);
    }


    /// <summary>
    /// Retorna a lista de produtos adicionados ao banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadProdutoDTO> RecuperaProdutos()
    {
        return Mapper.Map<List<ReadProdutoDTO>>(Context.Produtos);
    }

    
    /// <summary>
    /// Retorna um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaProdutos( int id)
    {
        var produto = Context.Produtos.FirstOrDefault(p=>p.id==id);
        if(produto is null) return NotFound();
        var Produtodto = Mapper.Map<ReadProdutoDTO>(produto);

        return Ok(Produtodto);
    }

    /// <summary>
    /// Atualiza um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a Atualização seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AtualizaProduto(int id, [FromBody] UpdateProdutoDTO produtoDTO)
    {
        var produto = Context.Produtos.FirstOrDefault(p => p.id == id);
        if(produto is null) return NotFound();

        Mapper.Map(produtoDTO, produto);
        Context.SaveChanges();
        return Ok(produtoDTO);
    }

    /// <summary>
    /// Deteleta um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a deleção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaProduto(int id, [FromBody] UpdateProdutoDTO produtoDTO)
    {
        var produto = Context.Produtos.FirstOrDefault(p => p.id == id);
        if (produto is null) return NotFound();

        Context.Remove(produto);
        Context.SaveChanges();
        return NoContent();
    }



}
