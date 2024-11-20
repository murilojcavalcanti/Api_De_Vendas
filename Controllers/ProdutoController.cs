using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Models;
using vendasApi.Repositories.unitOfWork;


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

            return new CreatedAtRouteResult(nameof(RecuperaProdutoPorId), new { id = responseProduto.Id }, responseProduto);

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
       /* try
        {*/
            IEnumerable<Produto> produtos = _unitOfWork.ProdutoRepository.GetAll();

            IEnumerable<ResponseProdutoDTO> responseProdutosDTO = _mapper.Map<IEnumerable<ResponseProdutoDTO>>(produtos);

            return Ok(responseProdutosDTO.ToList());
       /* }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "ocorreu um erro ao processar a sua solicitação!");
        }*/
    }


    /// <summary>
    /// Retorna um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResponseProdutoDTO> RecuperaProdutoPorId(int id)
    {
        try
        {
            if (id <= 0) return NotFound("O id incorreto");

            Produto produto = _unitOfWork.ProdutoRepository.Get(p => p.Id == id);

            if (produto is null) return NotFound();
            ResponseProdutoDTO Produtodto = _mapper.Map<ResponseProdutoDTO>(produto);

            return Ok(Produtodto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "ocorreu um erro ao processar a sua solicitação!");
        }

    }

    /// <summary>
    /// Atualiza um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a Atualização seja feita com sucesso</response>
    [HttpPut("{id}", Name = "RecuperaProduto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResponseProdutoDTO> AtualizaProduto(int id, [FromBody] UpdateProdutoDTO produtoDTO)
    {
        try
        {

            if (id <= 0) return NotFound("Produto não encontrado!");
            Produto produtoUpdate = _mapper.Map<Produto>(produtoDTO);
            produtoUpdate.Id = id;

            Produto produtoUpdated = _unitOfWork.ProdutoRepository.Update(produtoUpdate);
            _unitOfWork.Commit();

            ResponseProdutoDTO responseProdutoDTO = _mapper.Map<ResponseProdutoDTO>(produtoUpdated);
            return Ok(responseProdutoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "ocorreu um erro ao processar a sua solicitação!");
        }

    }

    /// <summary>
    /// Deteleta um produto, que foi buscado pelo id dentro banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a deleção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<ResponseProdutoDTO> DeletaProduto(int id)
    {
        try
        {
            Produto produto = _unitOfWork.ProdutoRepository.Get(p => p.Id == id);
            if (produto is null) return NotFound();

            ResponseProdutoDTO produtoDeleted = _mapper.Map<ResponseProdutoDTO>(_unitOfWork.ProdutoRepository.Delete(produto));

            return Ok(produtoDeleted);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "ocorreu um erro ao processar a sua solicitação!");
        }
    }



}
