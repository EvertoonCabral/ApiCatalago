using ApiCatalago.Context;
using ApiCatalago.Models;
using ApiCatalago.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        
        public ProdutoRepository(AppDbContext context) : base(context)
        {
            
        }

        public IEnumerable<Produto> GetProdutosPaginado(ProdutosParameters produtosParameters)
        {
            var produtos = GetAll()
                .OrderBy(p => p.Nome)
                .Skip((produtosParameters.pageNumber - 1) * produtosParameters.PageSize)
                .Take(produtosParameters.PageSize).ToList();

            return produtos;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
        {
            
            var produtos = _context.Produtos.Where(p => p.CategoriaId == idCategoria);
            return produtos;
            
        }
    }
}
