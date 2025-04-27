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

            try
            {
                var categorias = _context.Categorias.AsNoTracking().ToList(); //AsNoTracking limpa o cache da listagem.

                if (categorias is null)
                {
                    return NotFound("Nenhum produto encontrado...");
                }

                return categorias;

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar sua solicitação!");
            }



        }


        [HttpGet("{id}", Name = "Obter categoria")]
        public ActionResult<Categoria> GetCategoriaById(int id)
        {


            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

                if (categoria is null)
                {
                    return BadRequest("Nenhuma categoria encontrada");
                }


                return categoria;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar sua solicitação!");
            }


        }


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaWithProdutos()
        {

            try
            {
                var categorias = _context.Categorias.Include(p => p.Produtos).ToList();


                if (categorias is null)
                {
                    return NotFound("Nenhum produto encontrado...");
                }

                return categorias;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar sua solicitação!");
            }




        }


        [HttpPost]
        public ActionResult<Categoria> AddCategoria(Categoria categoria)
        {


            try
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
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar sua solicitação!");

            }

        }


        [HttpPut("{Id}")]
        public ActionResult EditCategoria(int Id, Categoria categoria)
        {


            try
            {
                if (categoria is null)
                {
                    return NotFound("categoria invalida");
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();


                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar sua solicitação!");

            }


        }



        [HttpDelete("{Id}")]
        public ActionResult removeCategoria(int Id)
        {


            try
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
            catch (Exception)
            {

                                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar sua solicitação!");

            }


        }

    }
}
