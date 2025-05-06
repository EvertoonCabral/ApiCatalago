using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {

            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {

            var produtos = await _context.Produtos.ToListAsync();

            if (produtos is null)
            {
                return NotFound("Nenhum produto encontrado...");
            }

            return  produtos;

        }


        [HttpGet("{id}", Name ="Obter Produtos")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId ==id);

            if (produto is null)
            {
                return NotFound("Nenhum produto encontrado...");
            }

            return  produto;

        }


        [HttpPost]
        public ActionResult AddProduto(Produto produto)
        {

            if (!ModelState.IsValid) //controller base ja faz essa validação
            {
                return BadRequest();
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();


            return new CreatedAtRouteResult("Obter Produtos",
                new { id = produto.ProdutoId }, produto);

        }


        [HttpPut("{id}")]
        public ActionResult UpdateProduto(int id, Produto produto)
        {
            if (produto.ProdutoId != id)
            {
                return BadRequest("Id do produto divergente");
            }

            if (produto is null)
            {
                return BadRequest("Produto invalido.");
            }


            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }


        [HttpDelete("{id}")]
        public ActionResult RemoveProduto(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return BadRequest("Produto Invalido");

            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();


            return Ok(produto);

        }
    }
}
