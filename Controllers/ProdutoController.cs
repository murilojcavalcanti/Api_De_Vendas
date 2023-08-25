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

    [HttpPost]
    public IActionResult AdicionaProduto(CreateProdutoDTO produtoDTO)
    {
        var produto = Mapper.Map<Produto>(produtoDTO);
        Context.Add(produto);
        Context.SaveChanges();
        return Ok(produto);
    }


}
