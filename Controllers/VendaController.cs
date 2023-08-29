using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using vendasApi.Data.Dtos.VendaDTO;
using vendasApi.Data.Dtos.VendaProdutoDTO;
using vendasApi.Enums;
using vendasApi.Models;

namespace vendasApi.Controllers;

[Controller]
[Route("[controller]")]
public class VendaController:ControllerBase
{
    private ApiVendasContext Context;
    private IMapper Mapper;

    public VendaController(ApiVendasContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma venda ao banco de dados
    /// </summary>
    /// <param name="vendaDTO">Objeto com os campos necessários para criação de uma venda</param>
    /// <param name="produtoId">Lista de Ids de produto para serem adicionados na venda</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaVenda([FromQuery] CreateVendaDTO vendaDTO, List<int> produtoId)
    {
        Venda venda = Mapper.Map<Venda>(vendaDTO);
        Context.Vendas.Add(venda);
        Context.SaveChanges();
        for (int i = 0; i < produtoId.Count; i++)
        {
            VendaProduto vendaProduto = new VendaProduto();
            vendaProduto.ProdutoId = produtoId[i];
            vendaProduto.VendaId = venda.Id;
            Context.VendasProdutos.Add(vendaProduto);
        }
        Context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaVendaPorId),
            new
            {
                id = venda.Id
            }, venda);
    }
    /// <summary>
    /// Adiciona um produto a uma venda que está no banco de dados
    /// </summary>
    /// <param name="VendaProdutoDTO"> Objetoo com os parametros de produtoid e VendaId</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost("/AdicionaProdutoVenda")]
    public IActionResult AdicionaProdutoVenda([FromQuery] CreateVendaProdutoDTO VendaProdutoDTO)
    {
        var vendaProduto = Mapper.Map<VendaProduto>(VendaProdutoDTO);
        Context.VendasProdutos.Add(vendaProduto);

        Context.SaveChanges();
        return Ok(vendaProduto);
    }


    /// <summary>
    /// Retorna a lista de vendas adicionadas ao banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadVendaDTO> RecuperaVendas( )
    {
        var Vendas = Mapper.Map<List<ReadVendaDTO>>(Context.Vendas.ToList());
        return Vendas;
    }

    /// <summary>
    /// Retorna uma venda com o indice escolhido que está no banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar a venda com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaVendaPorId( int id)
    {
        var venda = Context.Vendas.FirstOrDefault(venda=>venda.Id == id);
        if(venda is null) return NotFound();
        var vendaDto = Mapper.Map<ReadVendaDTO>(venda);
        return Ok(vendaDto);
    }

    /// <summary>
    /// Atualiza um venda do banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar o venda com esse indice</param>
    /// <param name="UpdatevendaDTO"> inteiro usado para definir o </param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AtulizaVenda(int id,[FromBody] UpdateVendaDTO UpdatevendaDTO)
    {
        var venda = Context.Vendas.FirstOrDefault(v=>v.Id==id);
        if(venda is null) return NotFound();
        if (venda.StatusVenda == Enums.StatusVendaEnum.AguardandoPagamento &&
            (UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.PagamentoAprovado|| UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.PagamentoAprovado))
        {
            Mapper.Map(UpdatevendaDTO, venda);
            Context.SaveChanges();
            return Ok(UpdatevendaDTO);
        }
        if (venda.StatusVenda == Enums.StatusVendaEnum.PagamentoAprovado &&
            (UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.EnviadoParaTransportadora || UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.Cancelada))
        {
            Mapper.Map(UpdatevendaDTO, venda);
            Context.SaveChanges();
            return Ok(UpdatevendaDTO);
        }
        if (venda.StatusVenda == Enums.StatusVendaEnum.EnviadoParaTransportadora &&
            (UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.Entregue ))
        {
            Mapper.Map(UpdatevendaDTO, venda);
            Context.SaveChanges();
            return Ok(UpdatevendaDTO);
        }

        return Ok("Atualização de venda Inválida");
    }

    /// <summary>
    /// Remove o venda do banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar a venda com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a requisição seja feita com sucesso</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public IActionResult DeletarVenda(int id)
    {
        var venda = Context.Vendas.FirstOrDefault(v=>v.Id==id);
        if( venda is null) return NotFound();
        Context.Remove(venda);
        Context.SaveChanges();
        return NoContent();

    }
}
