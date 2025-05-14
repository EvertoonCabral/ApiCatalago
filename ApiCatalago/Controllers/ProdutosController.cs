using ApiCatalago.Context;
using ApiCatalago.Models;
using ApiCatalago.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {

        private readonly IProdutoRepository _produtoRepository;
        public ProdutosController(IProdutoRepository produtoRepository)
        {

            _produtoRepository = produtoRepository;

        }


        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAllProdutos()
        {
            var produtos = _produtoRepository.GetAll().OrderBy(p => p.ProdutoId).ToList();
            if (produtos is null)
            {
                return NotFound();
            }
            return  Ok(produtos);

        }


        [HttpGet("{id}", Name ="Obter Produtos")]
        public ActionResult<Produto> GetProdutoById (int id)
        {
            var produto =  _produtoRepository.GetById(id);
            return  Ok(produto);

        }

        [HttpGet("categoria/{idCategoria}", Name = "Obter Produtos por Categoria")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = _produtoRepository.GetProdutosPorCategoria(idCategoria);
            if (produtos is null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }


        [HttpPost]
        public ActionResult AddProduto(Produto produto)
        {

          var novoProduto =  _produtoRepository.Create(produto);
            return new CreatedAtRouteResult("Obter Produtos", new {id = novoProduto.ProdutoId}, novoProduto);
            
        }


        [HttpPut("{id}")]
        public ActionResult UpdateProduto(int id, Produto produto)
        {
        
            var produtos = _produtoRepository.GetById(id);

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _produtoRepository.Update(produtos);     
            return Ok(produtos);

        }


        [HttpDelete("{id}")]
        public ActionResult RemoveProduto(int id)
        {
            var produto = _produtoRepository.GetById(id);

            _produtoRepository.Delete(produto);
            return Ok(produto);


        }
    }
}
