using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalago.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {

            var categorias = _context.Categorias.ToList();

            if (categorias is null)
            {
                return NotFound("Nenhum produto encontrado...");
            }

            return categorias;

        }


        [HttpGet("{id}", Name = "Obter categoria")]
        public ActionResult<Categoria> GetCategoriaById(int categoriaId)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == categoriaId);

            if (categoria is null)
            {
                return BadRequest("Nenhuma categoria encontrada");
            }


            return categoria;

        }

        [HttpPost]
        public ActionResult<Categoria> AddCategoria(Categoria categoria)
        {

            if (categoria is null)
            {

                return BadRequest("Categoria invalida");

            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("Obter categoria",
                    new { id = categoria.CategoriaId }, categoria);
        }


    }
}
