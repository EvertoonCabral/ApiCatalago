using ApiCatalago.Context;
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
    [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Categoria>> GetAllCategorias()
    {
        var categorias = _unitOfWork.CategoriaRepository.GetAll();
        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Categoria> GetCategoriaById(int id)
    {
        var categoria = _unitOfWork.CategoriaRepository.GetById(id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id= {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }

        return Ok(categoria);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult CreateCategoria(Categoria categoria)
    {
        if (categoria is null)
        {
            _logger.LogWarning("Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        _unitOfWork.CategoriaRepository.Create(categoria);
        _unitOfWork.Commit();

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult UpdateCategoria(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            _logger.LogWarning("Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        _unitOfWork.CategoriaRepository.Update(categoria);
        _unitOfWork.Commit();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteCategoria(int id)
    {
        var categoria = _unitOfWork.CategoriaRepository.GetById(id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        _unitOfWork.CategoriaRepository.Delete(categoria);
        _unitOfWork.Commit();

        return Ok(categoria);
    }
}
