using ApiVendasApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendasApi.Data.Dtos.ProdutoDTO;
using vendasApi.Data.Dtos.VendedorDTO;
using vendasApi.Models;
using vendasApi.Repositories.unitOfWork;

namespace vendasApi.Controllers;

[Controller]
[Route("[controller]")]
public class VendedorController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public VendedorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Adiciona um vendedor ao banco de dados
    /// </summary>
    /// <param name="vendedorDto">Objeto com os campos necessários para criação de um vendedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<ResponseVendedorDTO> AdicionaVendedor([FromBody] CreateVendedorDTO vendedorDTO)
    {
        try
        {
            Vendedor vendedor = _mapper.Map<Vendedor>(vendedorDTO);
            Vendedor vendedorCreated = _unitOfWork.VendedorRepository.Create(vendedor);
            _unitOfWork.Commit();

            ResponseVendedorDTO respondeVendedorDTO = _mapper.Map<ResponseVendedorDTO>(vendedorCreated);
            return CreatedAtAction("RecuperaVendedorPorId",
                new
                {
                   id = respondeVendedorDTO.Id
                }, respondeVendedorDTO);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
        }
    }



    /// <summary>
    /// Retorna a lista de vendedores adicionados ao banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ResponseVendedorDTO>> RecuperaVendedores(int take = 10)
    {
        try
        {

            List<Vendedor> vendedores = _unitOfWork.VendedorRepository.RecuperaVendedoresComVendas().Take(take).ToList();
            List<ResponseVendedorDTO> Vendedores = _mapper.Map<List<ResponseVendedorDTO>>(vendedores);
            return Vendedores;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
        }
    }

    /// <summary>
    /// Retorna um vendedor com o indice escolhido que está no banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar o vendedor com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpGet("{id}",Name = "RecuperaVendedorPorId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResponseVendedorDTO> RecuperaVendedorPorId(int id)
    {
        try
        {
            Vendedor vendedor = _unitOfWork.VendedorRepository.Get(vendedor => vendedor.Id == id);
            if (vendedor is null) return NotFound();
            ResponseVendedorDTO vendedorDto = _mapper.Map<ResponseVendedorDTO>(vendedor);
            return Ok(vendedorDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
        }
    }


    /// <summary>
    /// Atualiza um vendedor do banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar o vendedor com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a requisição seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResponseProdutoDTO> AtulizaVendedor(int id, [FromBody] UpdateVendedor vendedorDTO)
    {
        try
        {
            if (id <= 0) return NotFound("Id de vendedor incorreto");
            Vendedor vendedorUpdate = _mapper.Map<Vendedor>(vendedorDTO);
            vendedorUpdate.Id = id;
            Vendedor vendedorUpdated = _unitOfWork.VendedorRepository.Update(vendedorUpdate);
            _unitOfWork.Commit();
            ResponseVendedorDTO responseVendedorDTO = _mapper.Map<ResponseVendedorDTO>(vendedorUpdated);
            return Ok(responseVendedorDTO);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
        }
    }

    /// <summary>
    /// Remove o vendedor do banco de dados
    /// </summary>
    /// <param name="id"> inteiro usado para buscar o vendedor com esse indice</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a requisição seja feita com sucesso</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public IActionResult DeletarVendedor(int id)
    {
        try
        {
            Vendedor vendedor = _unitOfWork.VendedorRepository.Get(v => v.Id == id);
            if (vendedor is null) return NotFound();
            Vendedor vendedorDeleted=_unitOfWork.VendedorRepository.Delete(vendedor);
            ResponseVendedorDTO responseVendedorDTO = _mapper.Map<ResponseVendedorDTO>(vendedorDeleted);
            _unitOfWork.Commit();
            return Ok(responseVendedorDTO);
        }catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
        }
    }
}
