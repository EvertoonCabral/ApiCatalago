using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet("{Id}", Name = "Obter categoria")]
        public ActionResult<Categoria> GetCategoriaById(int categoriaId)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == categoriaId);

            if (categoria is null)
            {
                return BadRequest("Nenhuma categoria encontrada");
            }


            return categoria;

        }


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaWithProdutos()
        {

            var categorias = _context.Categorias.Include(p => p.Produtos).ToList();


            if (categorias is null)
            {
                return NotFound("Nenhum produto encontrado...");
            }

            return categorias;


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


        [HttpPut("{Id}")]
        public ActionResult EditCategoria(int Id, Categoria categoria)
        {

            if (categoria is null)
            {
                return NotFound("categoria invalida");
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();


            return Ok(categoria);
        }



        [HttpDelete("{Id}")]
        public ActionResult removeCategoria(int Id)
        {

            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == Id);


            if (categoria is null)
            {
                return NotFound("Categoria nao encontrada");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);

        }

    }
}
