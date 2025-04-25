using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("{id}")]
        public ActionResult<Produto> GetProdutoById(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId ==id);

            if (produto is null)
            {
                return NotFound("Nenhum produto encontrado...");
            }

            return produto;

        }

    }
}
