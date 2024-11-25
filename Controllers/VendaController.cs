using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendasApi.Data.Dtos.VendaDTO;
using vendasApi.Data.Dtos.VendaProdutoDTO;
using vendasApi.Models;
using vendasApi.Repositories.unitOfWork;

namespace vendasApi.Controllers;

[Controller]
[Route("[controller]")]
public class VendaController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    private IMapper Mapper;

    public VendaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        Mapper = mapper;
        _unitOfWork = unitOfWork;
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
    public ActionResult<ResponseVendaDTO> AdicionaVenda([FromQuery] CreateVendaDTO vendaDTO, List<int> ProdutoIds)
    {
        try
        {
            Venda venda = Mapper.Map<Venda>(vendaDTO);
            Venda VendaCreated = _unitOfWork.VendaRepository.Create(venda);
            foreach(int ProdutoId in ProdutoIds)
            {
                Produto produto = _unitOfWork.ProdutoRepository.Get(p => p.Id == ProdutoId);
                _unitOfWork.VendaRepository.AdicionaVendaProduto(venda, produto);
            }
            _unitOfWork.Commit();
            ResponseVendaDTO responseVendaDTO = Mapper.Map<ResponseVendaDTO>(VendaCreated);
            return CreatedAtAction("RecuperaVendaPorId",
                new
                {
                    id = responseVendaDTO.Id
                }, responseVendaDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao processar sua solicitação!");
        }
    }
    


    /// <summary>
    /// Retorna a lista de vendas adicionadas ao banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ResponseVendaDTO>> RecuperaVendas(int take = 10)
    {
        try
        {
            List<Venda> vendas = _unitOfWork.VendaRepository.RecuperaVendasComVendedor().Take(take).ToList();
            List<ResponseVendaDTO> responseVendas = Mapper.Map<List<ResponseVendaDTO>>(vendas);
            return responseVendas;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao processar sua solicitação!");
        }
    }

    /// <summary>
    /// Retorna uma venda com o indice escolhido que está no banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar a venda com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResponseVendaDTO> RecuperaVendaPorId(int id)
    {
        try
        {
            Venda venda = _unitOfWork.VendaRepository.Get(venda => venda.Id == id);
            if (venda is null) return NotFound();
            var vendaDto = Mapper.Map<ResponseVendaDTO>(venda);
            return Ok(vendaDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao processar sua solicitação!");
        }
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
    public ActionResult<ResponseVendaDTO> AtulizaVenda(int id, [FromBody] UpdateVendaDTO UpdatevendaDTO)
    {
        try
        {

            Venda venda = _unitOfWork.VendaRepository.Get(v => v.Id == id);
            Venda vendaUpdate = Mapper.Map<Venda>(UpdatevendaDTO);
            Venda vendaUpdated = null;

            if (venda is null) return NotFound();

            if (venda.StatusVenda == Enums.StatusVendaEnum.AguardandoPagamento &&
                (UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.PagamentoAprovado || UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.PagamentoAprovado))
            {
                vendaUpdated=_unitOfWork.VendaRepository.Update(vendaUpdate);
            }
            else if (venda.StatusVenda == Enums.StatusVendaEnum.PagamentoAprovado &&
                (UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.EnviadoParaTransportadora || UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.Cancelada))
            {
                vendaUpdated = _unitOfWork.VendaRepository.Update(vendaUpdate);
            }
            else if (venda.StatusVenda == Enums.StatusVendaEnum.EnviadoParaTransportadora &&
                (UpdatevendaDTO.StatusVenda == Enums.StatusVendaEnum.Entregue))
            {
                vendaUpdated = _unitOfWork.VendaRepository.Update(vendaUpdate);
            }
            

            if(!(vendaUpdated is null))
            {
                ResponseVendaDTO responseVendaDTO = Mapper.Map<ResponseVendaDTO>(vendaUpdated);
                return Ok(responseVendaDTO);
            }

                return BadRequest("Atualização de venda Inválida");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao processar sua solicitação!");

        }
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
        Venda venda = _unitOfWork.VendaRepository.Get(v => v.Id == id);
        if (venda is null) return NotFound();
        Venda vendaDeleted = _unitOfWork.VendaRepository.Delete(venda);
        _unitOfWork.Commit();
        return Ok(Mapper.Map<ResponseVendaDTO>(vendaDeleted));
    }
}
