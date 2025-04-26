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
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {

            var produtos = _context.Produtos.ToList();

            if (produtos is null)
            {
                return NotFound("Nenhum produto encontrado...");
            }

            return produtos;

        }


        [HttpGet("{id}", Name ="Obter Produtos")]
        public ActionResult<Produto> GetProdutoById(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId ==id);

            if (produto is null)
            {
                return NotFound("Nenhum produto encontrado...");
            }

            return produto;

        }


        [HttpPost]
        public ActionResult AddProduto(Produto produto)
        {
             
            {
                return BadRequest();
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();


            return new CreatedAtRouteResult("Obter Produtos",
                new { id = produto.ProdutoId }, produto);

        }


        [HttpPut("{id}")]
        public ActionResult UpdateProduto(int ProdutoId, Produto produto)
        {
            if (produto.ProdutoId != ProdutoId)
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


        [HttpDelete]
        public ActionResult RemoveProduto(int produtoId)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

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
