using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Models;
using vendasApi.Repositories.unitOfWork;
using vendasApi.Services;

namespace vendasApi.Controllers;
[ApiController]

[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;


    public ProdutoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Adiciona um Produto ao banco de dados
    /// </summary>
    /// <param name="ProdutoDto">Objeto com os campos necessários para criação de um vendedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<ResponseProdutoDTO> AdicionaProduto(CreateProdutoDTO produtoDTO)
    {
        try
        {
            if (produtoDTO is null) return BadRequest("Dados inválidos");

            Produto produto = _mapper.Map<Produto>(produtoDTO);
            _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();

            ResponseProdutoDTO responseProduto = _mapper.Map<ResponseProdutoDTO>(produto);

            return new CreatedAtRouteResult("RecuperaProduto", new { id = responseProduto.Id }, responseProduto);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
        }
    }


    /// <summary>
    /// Retorna a lista de produtos adicionados ao banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ResponseProdutoDTO>> RecuperaProdutos()
    {
        try
        {
            IEnumerable<Produto> produtos = _unitOfWork.ProdutoRepository.GetAll();
            IEnumerable<ResponseProdutoDTO> responseProdutosDTO = _mapper.Map<IEnumerable<ResponseProdutoDTO>>(produtos);

            return responseProdutosDTO.ToList();
        }
        catch (Exception)
        {
            return StatusCodes(StatusCodes.Status500InternalServerError,"ocorreu um erro ao processar a sua solicitação!");
        }
    }


    /// <summary>
    /// Retorna um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaProdutosPorId(int id)
    {
        var produto = Context.Produtos.FirstOrDefault(p => p.Id == id);
        if (produto is null) return NotFound();
        var Produtodto = Mapper.Map<ReadProdutoDTO>(produto);

        return Ok(Produtodto);
    }

    /// <summary>
    /// Atualiza um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a Atualização seja feita com sucesso</response>
    [HttpPut("{id}", Name = "RecuperaProduto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AtualizaProduto(int id, [FromBody] UpdateProdutoDTO produtoDTO)
    {
        var produto = Context.Produtos.FirstOrDefault(p => p.Id == id);
        if (produto is null) return NotFound();

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
        var produto = Context.Produtos.FirstOrDefault(p => p.Id == id);
        if (produto is null) return NotFound();

        Context.Remove(produto);
        Context.SaveChanges();
        return NoContent();
    }



}
