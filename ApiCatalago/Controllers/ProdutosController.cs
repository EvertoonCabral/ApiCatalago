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

        private readonly IProdutoRepository _repository;

        public ProdutosController(IProdutoRepository repository)
        {

            _repository = repository;

        }


        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {

            var produtos = _repository.GetProdutos().OrderBy(p => p.ProdutoId).ToList();
            return  Ok(produtos);

        }


        [HttpGet("{id}", Name ="Obter Produtos")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            var produto =  _repository.GetProduto(id);
            return  produto;

        }


        [HttpPost]
        public ActionResult AddProduto(Produto produto)
        {

            _repository.CreateProdutos(produto);
            return Ok(produto);
            
        }


        [HttpPut("{id}")]
        public ActionResult UpdateProduto(int id, Produto produto)
        {
        
            var produtos = _repository.GetProduto(id);
            
            _repository.UpdateProduto(produtos);     

            return Ok(produtos);

        }


        [HttpDelete("{id}")]
        public ActionResult RemoveProduto(int id)
        {
            var produto = _repository.GetProduto(id);

            _repository.DeleteProduto(id);


            return Ok(produto);

        }
    }
}
