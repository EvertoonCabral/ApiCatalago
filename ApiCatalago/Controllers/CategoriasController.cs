using ApiCatalago.Context;
using ApiCatalago.DTOs;
using ApiCatalago.DTOs.Mappings;
using ApiCatalago.Models;
using ApiCatalago.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{

    private UnitOfWork _unitOfWork;
    private readonly ILogger<CategoriasController> _logger;


    public CategoriasController(UnitOfWork unitOfWork, ILogger<CategoriasController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoriaDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>>GetAllCategorias()
    {
        var categorias =  await _unitOfWork.CategoriaRepository.GetAllAsync();

        if (categorias is null)
        {
            return NotFound("Categoria não encontrada");
        }
        var categoriasDto = categorias.ToCategoriaDtoList();
        
        return Ok(categoriasDto);
    }

    [HttpGet("{id}", Name = "ObterCategoria")]
    [ProducesResponseType(typeof(CategoriaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task <ActionResult<CategoriaDTO>> GetCategoriaById(int id)
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id= {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }
        
         var categoriaDto = categoria.ToCategoriaDto();

        return Ok(categoriaDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult CreateCategoria(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning("Dados inválidos...");
            return BadRequest("Dados inválidos");
        }
        var categoria = categoriaDto.ToCategoria();

        _unitOfWork.CategoriaRepository.Create(categoria);
        _unitOfWork.Commit();
        
        var categoriaDtoResult = categoria.ToCategoriaDto();

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDtoResult.CategoriaId }, categoriaDtoResult);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoriaDTO> UpdateCategoria(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            _logger.LogWarning("Dados inválidos...");
            return BadRequest("Dados inválidos");
        }
        
        var categoria = categoriaDto.ToCategoria();

        _unitOfWork.CategoriaRepository.Update(categoria);
        _unitOfWork.Commit();
        
        var categoriaDtoResult  = categoria.ToCategoriaDto();
        
        return Ok(categoriaDtoResult);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(CategoriaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDTO>> DeleteCategoria(int id)
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);
        
        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        _unitOfWork.CategoriaRepository.Delete(categoria);
        _unitOfWork.Commit();
        
        var categoriaDtoResult  = categoria.ToCategoriaDto();

        return Ok(categoriaDtoResult);
    }
}
