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

 
        private UnitOfWork _unitOfWork;

        public ProdutosController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        


        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAllProdutos()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll().OrderBy(p => p.ProdutoId).ToList();
            if (produtos is null)
            {
                return NotFound();
            }
            return  Ok(produtos);

        }


        [HttpGet("{id}", Name ="Obter Produtos")]
        public ActionResult<Produto> GetProdutoById (int id)
        {
            var produto =  _unitOfWork.ProdutoRepository.GetById(id);
            if (produto is null)
            {
                return NotFound("Produto nao encontrado");
            }
            return  Ok(produto);

        }

        [HttpGet("categoria/{idCategoria}", Name = "Obter Produtos por Categoria")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(idCategoria);
            if (produtos is null)
            {
                return NotFound("Produto nao encontrado");
            }
            return Ok(produtos);
        }


        [HttpPost]
        public ActionResult AddProduto(Produto produto)
        {

          var novoProduto =  _unitOfWork.ProdutoRepository.Create(produto);
          _unitOfWork.Commit();
            return new CreatedAtRouteResult("Obter Produtos", new {id = novoProduto.ProdutoId}, novoProduto);
            
        }


        [HttpPut("{id}")]
        public ActionResult UpdateProduto(int id, Produto produto)
        {
        
            var produtos = _unitOfWork.ProdutoRepository.GetById(id);

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _unitOfWork.ProdutoRepository.Update(produtos);     
            _unitOfWork.Commit();
            return Ok(produtos);

        }


        [HttpDelete("{id}")]
        public ActionResult RemoveProduto(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(id);

            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
            return Ok(produto);


        }
    }
}
