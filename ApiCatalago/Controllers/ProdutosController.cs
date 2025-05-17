using ApiCatalago.Context;
using ApiCatalago.DTOs;
using ApiCatalago.Models;
using ApiCatalago.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {

 
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public ProdutosController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        


        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> GetAllProdutos()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll().OrderBy(p => p.ProdutoId).ToList();
            if (produtos is null)
            {
                return NotFound();
            }
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);

        }


        [HttpGet("{id}", Name ="Obter Produtos")]
        public ActionResult<ProdutoDTO> GetProdutoById (int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(id);
            if (produto is null)
            {
                return NotFound("Produto nao encontrado");
            }
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDto);

        }

        [HttpGet("categoria/{idCategoria}", Name = "Obter Produtos por Categoria")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(idCategoria);
            if (produtos is null)
            {
                return NotFound("Produto nao encontrado");
            }
            return Ok(produtos);
        }


        [HttpPost]
        public ActionResult<ProdutoDTO> AddProduto(ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();
            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);
            return new CreatedAtRouteResult("Obter Produtos", new { id = novoProduto.ProdutoId }, novoProdutoDto);
        }


        [HttpPut("{id}")]
        public ActionResult<ProdutoDTO> UpdateProduto(int id, ProdutoDTO produtoDto)
        {
        
            var produto = _unitOfWork.ProdutoRepository.GetById(id);
            if (produto is null)
            {
                return NotFound();
            }
            _mapper.Map(produtoDto, produto);
            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();
            return Ok(_mapper.Map<ProdutoDTO>(produto));

        }


        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> RemoveProduto(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(id);
            if (produto is null)
            {
                return NotFound();
            }
            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDto);


        }
    }
}
